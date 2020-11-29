using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using ProductInventory.Domain.Models;

namespace ProductInventory.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly string _productUrl = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/products";

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _productUrl
                .SetQueryParam("token", "108337a6-dbcd-4231-92d4-d4962fc43b71")
                .AllowAnyHttpStatus()
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<Product[]>();
            return products;
        }
    }
}