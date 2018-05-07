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
        
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }
        
        public ActionResult Ban(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            if (user.IsBanned == true)
                return RedirectToAction("Index");

            return View(user);
        }
       
        [HttpPost, ActionName("Ban")]
        [ValidateAntiForgeryToken]
        public ActionResult BanConfirmed(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            user.IsBanned = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Disban(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            if (user.IsBanned == false)
                return RedirectToAction("Index");

            return View(user);
        }

        [HttpPost, ActionName("Disban")]
        [ValidateAntiForgeryToken]
        public ActionResult DisbanConfirmed(string id)
        {
            ApplicationUser user = db.Users.Find(id);
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
