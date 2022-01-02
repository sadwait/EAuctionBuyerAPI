using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerAPI.Models
{
    public class ProductInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
       
        [JsonProperty(PropertyName = "productName")]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }        

        [JsonProperty(PropertyName = "bidEndDate")]
        public DateTime BidEndDate { get; set; }
    }
}
