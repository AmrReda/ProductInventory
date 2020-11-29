using System.Collections.Generic;
using System.Threading.Tasks;
using ProductInventory.Domain.Models;

namespace ProductInventory.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}