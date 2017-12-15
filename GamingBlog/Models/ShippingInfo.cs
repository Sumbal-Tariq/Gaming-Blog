using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class ShippingInfo
    {
        public int ID { get; set; }
        public string ShippingType { get; set; }
        public float ShippingCost { get; set; }
        public int RegionID { get; set; }
        public DateTime Date { get; set; }
    }
}