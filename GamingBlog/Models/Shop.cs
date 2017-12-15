using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Shop
    {
        public int ShopID { get; set; }
        [Required]
        public string ShopTitle { get; set; }
        public List<Product> Products { get; set; }
        public List<PCategory> Categories { get; set; }
    }
}