using System;

namespace AccountsAPI.Models
{
    public class Buyer
    {
        public string id { get; set; }
        public string ProductId { get; set; } 
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }
        public string StartingPrice { get; set; }
        public DateTime BidEndDate { get; set; }
    }
}
