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
            Product product = new Product();

            // preluam lista de categorii din metoda GetAllCategories()
            product.Categories = GetAllCategories();

            return View(product);
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


        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();

            // Extragem toate categoriile din baza de date
            var categories = from cat in db.Categories
                             select cat;

            // iteram prin categorii
            foreach (var category in categories)
            {
                // Adaugam in lista elementele necesare pentru dropdown
                selectList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name.ToString()
                });
            }

            // returnam lista de categorii
            return selectList;
        }
    }
}