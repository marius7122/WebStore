using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        public virtual ICollection<Review> Reviews { get; set; }
    }

    public class Review
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Rating { get; set; }

        public virtual Product Product { get; set; }
    }
}