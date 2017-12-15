using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Rating
    {
        public int ID { get; set; }

        public int PostID { get; set; }

        public string UserID { get; set; }

        public string UserName { get; set; }

        public int Rate { get; set;}
    }
}