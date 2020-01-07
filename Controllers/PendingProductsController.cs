using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PendingProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Approve(int id)
        {
            Product product = db.PendingProducts.Find(id);
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        public ActionResult Delete(int id)
        {
            Product product = db.PendingProducts.Find(id);
            db.PendingProducts.Remove(product);
            TempData["message"] = "Produsul a fost sters!";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}