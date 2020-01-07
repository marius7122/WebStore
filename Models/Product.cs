using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStore.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required][MaxLength(100)][MinLength(4)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Display(Name = "Rating")]
        public double AverageRating { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}