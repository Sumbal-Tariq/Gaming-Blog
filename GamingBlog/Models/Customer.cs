using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        [DataType(DataType.CreditCard)]
        [Required]
        [DisplayName("Credit Card")]
        public string CreditCard { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email ID")]
        public string userEmail { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public string userName { get; set; }
        public string userId { get; set; }
    }
}