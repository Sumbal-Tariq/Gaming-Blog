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
    public class TrashesController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // POST: Trashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trash trash = db.Trashes.Find(id);
            db.Trashes.Remove(trash);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            Trash trash = db.Trashes.Find(id);
            Post post = new Post();
            post.PostCategory = trash.PostCategory;
            post.PostContent = trash.PostContent;
            post.PostID = trash.TrashID;
            post.PostStatus = trash.PostStatus;
            post.PostTitle = trash.PostTitle;
            post.ImageUrl = trash.ImageUrl;
            var ratings = db.Ratings.Where(d => d.PostID.Equals(id)).ToList();
            ViewBag.ratings = ratings;
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rate);
                var ratingCount = ratings.Count();
                double rating = ratingSum / ratingCount;
                post.Rating = rating.ToString();
            }
            db.Posts.Add(post);
            db.SaveChanges();
            db.Trashes.Remove(trash);
            db.SaveChanges();
            return RedirectToAction("Index");
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
