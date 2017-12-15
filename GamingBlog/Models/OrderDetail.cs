using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public float UnitCost { get; set; }
        public float TotalCost { get; set; }
        public int noOfItems { get; set; }
        public List<Product> Products { get; set; }
    }
}