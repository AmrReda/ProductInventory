using System.Collections.Generic;
using ProductInventory.Domain.Models;

namespace ProductInventory.Domain.Queries.GetSortedProduct
{
    public class GetSortedProductQueryResponse
    {
        public GetSortedProductQueryResponse(IEnumerable<Product> products)
        {
            Products = products;
        }

        public IEnumerable<Product> Products { get; }
    }
}