﻿using Microsoft.AspNet.Identity.Owin;
using NewAuction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace NewAuction.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        private ApplicationUserManager _userManager;
        ApplicationDbContext applicationContext = new ApplicationDbContext();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Product
        [AllowAnonymous]
        public ActionResult Index(Int32 productId)
        {
            Product currentProduct = applicationContext.Product.First(x => x.ID == productId && x.IsActive == true);
            if (DateTime.Now.CompareTo(currentProduct.StartAuction.AddHours(1)) == 1)
            {
                currentProduct.IsActive = false;
                Bet lastBetPerProduct = currentProduct.Bet.Last(x => x.Product.ID == currentProduct.ID);
                if (lastBetPerProduct != null)
                {
                    currentProduct.Buyer = lastBetPerProduct.User.Id;
                }
                applicationContext.SaveChanges();
            }
            if (currentProduct != null && currentProduct.IsActive)
            {
                return View(currentProduct);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Index(double bet, double productId)
        {
            var product = applicationContext.Product.First(x => x.ID == productId);
            if (bet <= product.SoldPrice)
            {
                ViewBag.Error = "Bet must be higher than price";
                return View(product);
            }
            if (DateTime.Now.CompareTo(product.StartAuction.AddHours(1)) == 1)
            {
                product.IsActive = false;
                Bet lastBetPerProduct = product.Bet.Last(x => x.Product.ID == product.ID);
                if (lastBetPerProduct != null)
                {
                    product.Buyer = lastBetPerProduct.User.Id;
                }
                applicationContext.SaveChanges();
                return View(product);
            }
            product.SoldPrice = bet;
            Bet newBet = new Bet();
            newBet.Price = bet;
            newBet.Product = product;
            newBet.TimeStamp = DateTime.Now.Millisecond;
            String id = User.Identity.GetUserId();
            newBet.User = applicationContext.Users.First(x => x.Id == id);
            applicationContext.Bet.Add(newBet);
            applicationContext.SaveChanges();
            return View(product);
        }
    }
}