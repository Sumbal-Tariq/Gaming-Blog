using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class PCategory
    {
        public int PCategoryID { get; set; }
        public String PCategoryTitle { get; set; }
        public List<Product> Products { get; set; }
    }
}