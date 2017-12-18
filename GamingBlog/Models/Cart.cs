using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
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
        public List<Product> Products { get; set; }
    }
}