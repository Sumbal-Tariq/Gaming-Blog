using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class User:Person
    {
        public Blog blog;
        public Post post;

    }
}