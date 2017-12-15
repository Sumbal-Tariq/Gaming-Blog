using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Admin:Person
    {
        public Post post;
        public Blog blog;
    }
}