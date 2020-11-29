using System.Linq;
using System.Threading.Tasks;
using ProductInventory.Domain.Services;

namespace ProductInventory.Domain
{
    public class GetSortedProductQueryHandler
    {
        private readonly IProductService _productService;

        public GetSortedProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetSortedProductQueryResponse> Handle(GetSortedProductQuery getSortedProductQuery)
        {
            var products = await _productService.GetProducts();
            var productList = products.ToList();
            return getSortedProductQuery.SortOption switch
            {
                "Low" => new GetSortedProductQueryResponse(productList.OrderBy(product => product.Price)),
                "High" => new GetSortedProductQueryResponse(productList.OrderByDescending(product => product.Price)),
                "Ascending" => new GetSortedProductQueryResponse(productList.OrderBy(product => product.Name)),
                "Descending" => new GetSortedProductQueryResponse(productList.OrderByDescending(product => product.Name)),
                //TODO: "Recommended" => new GetSortedProductQueryResponse(await _recommendationsService.Recommend(productList)),
                _ => new GetSortedProductQueryResponse(productList)
            };
        }
    }
}