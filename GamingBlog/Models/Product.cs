using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("Product Price")]
        public float ProductPrice { get; set; }
        [Required]
        [DisplayName("Product Quantity")]
        public int quantity { get; set; }
        [Required]
        [DisplayName("Item Status")]
        public string itemStatus { get; set; }
        [Required]
        [DisplayName("Product Status")]
        public string ProductStatus { get; set; }
        [DisplayName("Select Image ")]
        public string ImageUrl { get; set; }
        public string TrashStatus { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string ProductDescription { get; set; }
        [DisplayName("Product Category")]
        [Required]
        public String ProductCategory { get; set; }

        // default constructor
        public Product()
        {
            Date = DateTime.Now;
        }
    }
}