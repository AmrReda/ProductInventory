using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using ProductInventory.Domain.Models;

namespace ProductInventory.Domain.Services
{
    public class ShopperHistoryService : IShopperHistoryService
    {
        private readonly string _shopperHistorysUrl = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/shopperHistory";

        public async Task<IEnumerable<ShopperHistory>> GetShopperHistory()
        {
            var shopperHistories = await _shopperHistorysUrl
                .SetQueryParam("token", "108337a6-dbcd-4231-92d4-d4962fc43b71")
                .AllowAnyHttpStatus()
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<List<ShopperHistory>>();
            return shopperHistories;
        }
    }
}