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
                if (DateTime.Now.CompareTo(current.StartAuction.AddHours(1)) == 1)
                {
                    current.IsActive = false;
                    Bet lastBetPerProduct = current.Bet.Last();
                    if (lastBetPerProduct != null) {
                        current.Buyer = lastBetPerProduct.User;
                    }
                }
                if (current.IsActive && current.Buyer == null)
                {
                    ProductViewModel product = new ProductViewModel();
                    product.Description = current.Description;
                    product.ID = current.ID;
                    product.ImageUrl = current.Image;
                    product.Name = current.Name;
                    product.StartTime = current.StartAuction;
                    product.CurrentPrice = current.SoldPrice;
                    product.StartPrice = current.StartPrice;

                    model.Add(product);
                }
            }
            applicationContext.SaveChanges();
            return View(model);
        }
    }
}