using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingBlog.Models;

namespace GamingBlog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashDashesController : Controller
    {
        private BlogDBContext db = new BlogDBContext();
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: DashDashes
        public ActionResult Index()
        {
            //var users = context.Users.ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: DashDashes
        public ActionResult users()
        {
            List<Person> lstUsers = new List<Person>();
            var users = context.Users.ToList();
            foreach (var a in users) {
                Person p = new Person();
                p.UserName = a.UserName;
                p.UserEmail = a.Email;
                p.password = a.PasswordHash;
                p.ProfilePicture = a.ProfilePicture;
                lstUsers.Add(p);
            }

            ViewBag.totalUsers = users.Count;
            ViewBag.RegUsers = lstUsers;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
