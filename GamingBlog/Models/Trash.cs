using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Trash
    {
        [Required]
        public int TrashID { get; set; }
        [Required]
        [DisplayName("Post Title   ")]
        public string PostTitle { get; set; }
        [Required]
        [DisplayName("Post Content")]
        [DataType(DataType.MultilineText)]
        public string PostContent { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [DisplayName("Post Category")]
        [Required]
        public String PostCategory { get; set; }
        [DisplayName("Select Image ")]
        public string ImageUrl { get; set; }
        [Required]
        [DisplayName("Post Status  ")]
        public string PostStatus { get; set; }
        // default constructor
        public Trash()
        {
            Date = DateTime.Now;
            AuthorName = "Admin";
        }
    }
}