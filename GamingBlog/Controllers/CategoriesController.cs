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
    [Authorize(Roles ="Admin")]
    public class CategoriesController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        [Authorize(Roles = "Admin")]
        public ActionResult IndexAdmin()
        {
            return View(db.Categories.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Categories/Details/5
        public ActionResult DetailsAdmin(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var posts = from m in db.Posts
                        select m;
            // use only those post for display with status of published
            posts = posts.Where(s => s.PostCategory.Equals(category.Title));
            ViewBag.post = posts;
            return View();
        }


        [Authorize(Roles = "Admin")]
        // GET: Categories/Create
        public ActionResult CreateAdmin()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin([Bind(Include = "ID,Title")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }

            return View(category);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdmin([Bind(Include = "ID,Title")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAdmin(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("IndexAdmin");
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
