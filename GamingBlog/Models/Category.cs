using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Category Title")]
        public string Title { get; set; }
        [DisplayName("List of Posts in Cateogry")]
        public List<Post> Posts { get; set; }
    }
}