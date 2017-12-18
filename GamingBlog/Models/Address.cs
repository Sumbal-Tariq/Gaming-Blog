using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Address
    {
        public int id { get; set; }
        [DataType(DataType.PostalCode)]
        [Required]
        [DisplayName("Postal Code")]
        public int ZipCode { get; set; }
        [Required]
        [DisplayName("City")]
        public string City { get; set; }
        [Required]
        [DisplayName("Country")]
        public string Country { get; set; }
        public string userid { get; set; }
        [Required]
        [DisplayName("Address")]
        public string _Address { get; set; }
    }
}