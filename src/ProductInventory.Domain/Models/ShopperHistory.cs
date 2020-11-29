using System.Collections.Generic;

namespace ProductInventory.Domain.Models
{
    public class ShopperHistory
    {
            public ShopperHistory(string customerId, List<Product> products)
            {
                CustomerId = customerId;
                Products = products;
            }

            public string CustomerId { get; private set; }
            public List<Product> Products { get; private set; }
    }
}