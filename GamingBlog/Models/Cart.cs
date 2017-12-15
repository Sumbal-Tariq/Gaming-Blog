using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public float UnitCost { get; set; }
        public float totalCost { get; set; }
        public List<Product> Products { get; set; }
    }
}