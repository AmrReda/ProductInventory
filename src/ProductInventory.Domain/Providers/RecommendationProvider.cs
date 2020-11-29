using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductInventory.Domain.Models;
using ProductInventory.Domain.Services;

namespace ProductInventory.Domain.Providers
{
    public class RecommendationProvider
    {
        private readonly IShopperHistoryService _shopperHistoryService;

        public RecommendationProvider(IShopperHistoryService shopperHistoryService)
        {
            _shopperHistoryService = shopperHistoryService;
        }
        
        public async Task<IEnumerable<Product>> Recommend(IEnumerable<Product> products)
        {
            var shopperHistory = await _shopperHistoryService.GetShopperHistory();

            var productsOrderedBasedOnNumberOfOrders = from shoppingHistory in shopperHistory
                let allOrders = shoppingHistory.Products
                from order in allOrders
                group order by order.Name into ordersGroupedByName
                let productsAndNumberOfOrders =new
                {
                    NumberOfOrders = ordersGroupedByName.Sum(product => product.Quantity),
                    Product = products.SingleOrDefault(product => product.Name == ordersGroupedByName.Key)
                }  
                orderby productsAndNumberOfOrders.NumberOfOrders descending 
                select productsAndNumberOfOrders.Product;
            
            var orderedProducts = productsOrderedBasedOnNumberOfOrders.ToList();
            var productsThatWereNotOrdered = products.Except(orderedProducts);
            orderedProducts.AddRange(productsThatWereNotOrdered);
            return orderedProducts;
        }
    }
}