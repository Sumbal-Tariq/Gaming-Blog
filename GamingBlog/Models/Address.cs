using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Address
    {
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string _Address { get; set; }
    }
}