using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Customer
    {
        [DataType(DataType.CreditCard)]
        public string CreditCard { get; set; }
        public Address Address_obj { get; set; }
        public List<Order> Orders { get; set; }
    }
}