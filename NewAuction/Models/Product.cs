﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewAuction.Models
{
    public class Product
    {
        [Key]
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Image { get; set; }
        public String Description { get; set; }
        public Double StartPrice { get; set; }
        public Double SoldPrice { get; set; }
        public Boolean IsActive { get; set; }
        
        public virtual ApplicationUser User { get; set; }
    }
}