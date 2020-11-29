using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProductInventory.Domain.Models
{
    public class Special
    {
        public Special()
        {
            
        }
        public Special(List<Quantity> quantities, double total)
        {
            Quantities = quantities;
            Total = total;
        }
        
        [JsonProperty] 
        public List<Quantity> Quantities { get; set; }
        [JsonProperty] 
        public double Total { get; set; }
    }
}