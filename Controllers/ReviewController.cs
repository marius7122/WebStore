using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                TempData["message"] = "Comment was submited!";

                // update product rating
                Product product = db.Products.Find(review.ProductID);
                product.AverageRating = ProductController.AverageRating(review.ProductID);
                UpdateModel(product);
                db.SaveChanges();
            }
            else
            {
                TempData["message"] = "Comment was not submited!";
            }
            return RedirectToAction("Show", "Product", new { id = review.ProductID });
        }

        public ActionResult Edit(int id)
        {
            var review = db.Reviews.Find(id);
            return View(review);
        }

        [HttpPut]
        public ActionResult Edit(int id, Review modifiedReview)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Review review = db.Reviews.Find(id);
                    if (TryUpdateModel(review))
                    {
                        TempData["message"] = "Review was updated!";
                        db.SaveChanges();
                    }
                    return RedirectToAction("Show", "Product", new { id = review.ProductID });
                }
                else
                {
                    return View(modifiedReview);
                }
            }
            catch (Exception e)
            {
                return View(modifiedReview);
            }
        }

        public ActionResult Delete(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Show", "Product", new { id = review.ProductID });
        }
    }
}