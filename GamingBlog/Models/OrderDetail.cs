using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("Product ID")]

        public int itemid { get; set; }
        [DisplayName("Product Name")]
        public string cartItemName { get; set; }
        [DisplayName("Product Quantity")]
        public int itemQuantity { get; set; }
        [DisplayName("Unit Cost")]
        public float UnitCost { get; set; }
        public string userID { get; set; }
        [DisplayName("Image")]
        public string itemImage { get; set; }
        [DisplayName("Total Cost")]
        public float totalCost { get; set; }
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
        public List<Product> Products { get; set; }
    }
}