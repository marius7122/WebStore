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

        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            return View(product);
        }

        [HttpPut]
        public ActionResult Edit(int id, Product modifiedProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = db.Products.Find(id);
                    if (TryUpdateModel(product))
                    {
                        product.Title = modifiedProduct.Title;
                        product.Description = modifiedProduct.Description;
                        product.Price = modifiedProduct.Price;
                        TempData["message"] = "Produsul a fost modificat!";
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(modifiedProduct);
                }
            }
            catch (Exception e)
            {
                return View(modifiedProduct);
            }
        }

        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            TempData["message"] = "Produsul a fost sters!";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}