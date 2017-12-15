using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime Date { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public List<Product> Products { get; set; }
    }
}