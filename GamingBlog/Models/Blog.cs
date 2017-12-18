using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GamingBlog.Models
{
    public class Blog
    {
        public int BlogID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string BlogDescription { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("List of Published Posts")]
        List<Post> Posts { get; set; }
        [DisplayName("List of Post Categories")]
        List<Category> Categories { get; set; }

        // default Constructor
        public Blog()
        {
            Date = DateTime.Now;
        }

    }
    public class BlogDBContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Rating> Ratings { set; get;}
        public DbSet<DashDash> DashDashes { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<PCategory> PCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public System.Data.Entity.DbSet<GamingBlog.Models.Trash> Trashes { get; set; }
    }

}