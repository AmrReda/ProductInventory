using Newtonsoft.Json;

namespace ProductInventory.Domain.Models
{
    public class Product
    {
        [JsonProperty] public string Name { get; }
        [JsonProperty] public double Price { get; }
        [JsonProperty] public double Quantity { get; }

        public Product(string name, double price, double quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}