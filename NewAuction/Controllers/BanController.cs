using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewAuction.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NewAuction.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class BanController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string sortOrder, int id = -1)
        {
            IQueryable<ApplicationUser> students = null;
            var products = from s in db.Product where s.Buyer != null select s;
            ViewBag.Auctions = products;
            ViewBag.productId = id;
            ViewBag.DateSortParam = sortOrder == "Address" ? "address_desc" : "Address";
            if (id == -1)
            {
                students = from s in db.Users select s;
            }
            else
            { 

                var auction = products.Where(x => x.ID == id).First();
                var usersInAuction = from s in db.Bet where s.Product.ID == auction.ID select s.User;


                students = from s in db.Users
                               where usersInAuction.Contains(s)
                               select s;
            }

            switch (sortOrder)
            {
                case "Address":
                    students = students.OrderBy(s => s.Address);
                    break;
                case "address_desc":
                    students = students.OrderByDescending(s => s.Address);
                    break;
                default:
                    students = students.OrderBy(s => s.Email);
                    break;
            }
            return View(students.ToList());
        }

        public ActionResult BanUser(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            if (user.IsBanned == true)
                return RedirectToAction("Index");

            user.IsBanned = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Disban(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            if (user.IsBanned == false)
                return RedirectToAction("Index");

            user.IsBanned = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
