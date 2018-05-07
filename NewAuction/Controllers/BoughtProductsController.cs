using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewAuction.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace NewAuction.Controllers
{
    public class BoughtProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BoughtProducts
        public ActionResult Index()
        {
            String id = User.Identity.GetUserId();
            return View(db.Product.Where(x => x.Buyer == id).ToList());
        }

        // GET: BoughtProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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
