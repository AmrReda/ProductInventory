using System.Collections.Generic;
using Newtonsoft.Json;
using ProductInventory.Domain.Models;

namespace ProductInventory.Domain.Queries.CalculateTrolley
{
    public class CalculateTrolleyQuery
    {
        [JsonProperty] 
        public List<Product> Products { get; set; }
        [JsonProperty] 
        public List<Special> Specials { get; set; }
        [JsonProperty] 
        public List<Quantity> Quantities { get; set; }
    }
}