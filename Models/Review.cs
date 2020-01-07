using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class Review
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}