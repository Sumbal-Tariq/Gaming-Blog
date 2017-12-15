using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [DisplayName("Type Your Comment")]
        [DataType(DataType.MultilineText)]
        public string ContentOfComment { set; get; }

        [DisplayName("Author Name")]
        public string AuthorName { set; get; }

        public string UserID { set; get; }

        public string UserProfilePicture { set; get; }

        public int PostID { get; set; }

        public DateTime DateOfComment { set; get; }
        // Default Constructor

    }
}