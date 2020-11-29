using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ProductInventory.Domain.Models
{
    public class Quantity
    {
        public Quantity()
        {
            
        }
        public Quantity(string name, double quantity)
        {
            Name = name;
            Value = quantity;
        }

        [JsonProperty] 
        public string Name { get; set; }
        [JsonPropertyName("quantity")] 
        public double Value { get; set; }
    }
}