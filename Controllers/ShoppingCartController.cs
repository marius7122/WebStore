using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            string loggedUser = User.Identity.GetUserId();
            var products = db.shoppingCartProducts
                .Include("Product")
                .Where(m => m.UserId == loggedUser);

            return View(products);
        }

        public ActionResult Delete(int id)
        {
            ShoppingCartProduct product = db.shoppingCartProducts.Find(id);
            db.shoppingCartProducts.Remove(product);
            TempData["message"] = "Produsul a fost sters!";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int ProductId)
        {
            ShoppingCartProduct prod = new ShoppingCartProduct();
            prod.ProductId = ProductId;
            prod.UserId = User.Identity.GetUserId();

            db.shoppingCartProducts.Add(prod);
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}