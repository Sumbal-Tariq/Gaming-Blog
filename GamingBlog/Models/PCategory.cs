using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class PCategory
    {
        public int PCategoryID { get; set; }
        [Required]
        [DisplayName("Product Category Title")]
        public String PCategoryTitle { get; set; }
        public List<Product> Products { get; set; }
    }
}