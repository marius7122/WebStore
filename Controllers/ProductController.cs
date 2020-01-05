using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var products = from product in db.Products
                           select product;

            return View(products);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return Redirect("/Product/Show/" + product.ID);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        
        public ActionResult Show(int id)
        {
            Product product = db.Products.Find(id);
            return View(product);
        }
    }
}