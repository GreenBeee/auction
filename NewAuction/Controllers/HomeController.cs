using NewAuction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAuction.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext applicationContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = new List<ProductViewModel>();
            foreach (var current in applicationContext.Product)
            {
                if (current.IsActive)
                {
                    ProductViewModel product = new ProductViewModel();
                    product.Description = current.Description;
                    product.ID = current.ID;
                    product.ImageUrl = current.Image;
                    product.Name = current.Name;
                    product.CurrentPrice = current.SoldPrice;
                    product.StartPrice = current.StartPrice;

                    model.Add(product);
                }
            }
            return View(model);
        }
    }
}