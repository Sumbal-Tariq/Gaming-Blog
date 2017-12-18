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
        [Key]
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
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Required]
        [DisplayName("Phone Number")]
        public string phn_No { get; set; }
        [DisplayName("Address Id")]
        public int address { get; set; }
        public string userId { get; set; }
    }
}