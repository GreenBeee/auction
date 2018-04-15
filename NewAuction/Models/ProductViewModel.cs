using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAuction.Models
{
    public class ProductViewModel
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String ImageUrl { get; set; }
        public String Description { get; set; }
        public Double StartPrice { get; set; }
        public Double CurrentPrice { get; set; }
    }
}