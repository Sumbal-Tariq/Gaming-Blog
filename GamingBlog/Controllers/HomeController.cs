using GamingBlog.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GamingBlog.Controllers
{
    public class HomeController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        public ActionResult Index(string postCategory, string SearchString,int? page)
        {

            var categoryLst = new List<string>();

            var categoryQry = from d in db.Posts
                           orderby d.PostCategory
                           select d.PostCategory;

            categoryLst.AddRange(categoryQry.Distinct());
            ViewBag.movieGenre = categoryLst;

            var posts = from m in db.Posts
                         select m;
            // use only those post for display with status of published
            posts = posts.Where(s => s.PostStatus.Equals("Published"));

            if (!String.IsNullOrEmpty(SearchString))
            {
                posts = posts.Where(s => s.PostTitle.Contains(SearchString));
                posts = posts.Where(s => s.PostStatus.Equals("Published"));
            }

            if (!string.IsNullOrEmpty(postCategory))
            {
                posts = posts.Where(x => x.PostCategory == postCategory);
            }
            posts = posts.OrderByDescending(m => m.Date);
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            var allratings = db.Ratings.Where(d => d.PostID == id && d.UserID == userId).ToList();
            if (allratings.Count == 1)
            {
                ViewBag.AlreadyRated = true;
            }
            else
            {
                ViewBag.AlreadyRated = false;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rate = post.Rating;
            ViewBag.ArticleId = id.Value;
            var commentss = db.Comments.Where(d => d.PostID.Equals(id.Value)).ToList();
            var ratings = db.Ratings.Where(d => d.PostID.Equals(id.Value)).ToList();
            ViewBag.ratings = ratings;
            ViewBag.Commentss = commentss;
            post.TotalComments = commentss.Count();
            ViewBag.tcomments = post.TotalComments;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rate);
                var ratingCount = ratings.Count();
                double rating = ratingSum / ratingCount;
                post.Rating = rating.ToString();
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            return View(post);

        }
    }
}