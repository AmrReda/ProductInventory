using Newtonsoft.Json;

namespace ProductInventory.Domain.Models
{
    public class Product
    {
        public Product()
        {
        }
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
        public Product(string name, double price, double quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        
        [JsonProperty] 
        public string Name { get; }
        [JsonProperty] 
        public double Price { get; }
        [JsonProperty] 
        public double Quantity { get; }
    }
}