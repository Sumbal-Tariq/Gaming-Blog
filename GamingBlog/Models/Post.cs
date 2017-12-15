using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Post
    {
        [Required]
        public int PostID { get; set; }
        [Required]
        [DisplayName("Post Title   ")]
        public string PostTitle { get; set; }
        [Required]
        [DisplayName("Post Content")]
        [DataType(DataType.MultilineText)]
        public string PostContent { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Post Category")]
        [Required]
        public String PostCategory { get; set; }
        public string Rating { get; set; }
        [DisplayName("List of Given Comments")]
        public List<Comment> Comments { get; set; }
        [DisplayName("Select Image ")]
        public string ImageUrl { get; set; }
        [Required]
        [DisplayName("Post Status  ")]
        public string PostStatus { get; set; }
        public int TotalComments { get; set; }
        // default constructor
        public Post()
        {
            Date = DateTime.Now;
            AuthorName = "Admin";
        }
    }
}