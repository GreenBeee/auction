using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAuction.Models
{
    public class Ban
    {
        public virtual ApplicationUser User { get; set; }
    }
}