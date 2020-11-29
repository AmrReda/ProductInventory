using System.Collections.Generic;
using System.Threading.Tasks;
using ProductInventory.Domain.Models;

namespace ProductInventory.Domain.Services
{
    public interface IShopperHistoryService
    {
        Task<IEnumerable<ShopperHistory>> GetShopperHistory();
    }
}