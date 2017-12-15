using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Person
    {
        public int UserID { get; set; }
        public String UserName { get; set; }
        public String UserEmail { get; set; }
        public String password { get; set; }
        public string ProfilePicture { get; set; }
    }
}