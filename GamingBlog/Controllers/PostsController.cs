using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingBlog.Models;
using System.IO;
using PagedList;
using Microsoft.AspNet.Identity;

namespace GamingBlog.Controllers
{

    public class PostsController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Posts
        public ActionResult index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var posts = from s in db.Posts select s;
            posts = posts.Where(s => s.PostStatus.Equals("Published"));
            posts = posts.OrderByDescending(m => m.Date);
            return View(posts.ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Display(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 15;
            var posts = from s in db.Posts select s;
            posts = posts.OrderByDescending(s => s.Date);
            ViewBag.TotalPosts = posts.Count();
            ViewBag.Published = posts.Where(s => s.PostStatus.Equals("Published"));
            ViewBag.PublishedPosts = posts.Where(s => s.PostStatus.Equals("Published")).Count();
            ViewBag.Draft = posts.Where(s => s.PostStatus.Equals("Draft"));
            ViewBag.DraftPosts = posts.Where(s => s.PostStatus.Equals("Draft")).Count();
            ViewBag.Draft = posts.Where(s => s.PostStatus.Equals("Draft"));
            ViewBag.DraftPosts = posts.Where(s => s.PostStatus.Equals("Draft")).Count();
            ViewBag.Trash = db.Trashes.ToList();
            ViewBag.TrashPosts = db.Trashes.ToList().Count();
            return View(posts.ToPagedList(pageNumber,pageSize));
        }

        // GET: Posts/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult DetailsAdmin(int? id)
        { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult NewPost()
        {
            // This code is for the categories drop down menu
            var categoriesLst = new List<string>();

            var categoryQry = from d in db.Categories
                              orderby d.Title
                              select d.Title;
            categoriesLst.AddRange(categoryQry.Distinct());
            ViewBag.PostCategories = categoriesLst;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost([Bind(Include = "PostID,PostTitle,PostContent,AuthorName,Date,PostCategory,ImageUrl,PostStatus,Rating")] Post post, HttpPostedFileBase imgFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(imgFile.FileName);
            string extension = Path.GetExtension(imgFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            post.ImageUrl = "/img/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/img/"), fileName);
            imgFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Display");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        [Authorize(Roles = "Admin")]
        public ActionResult EditAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var categoriesLst = new List<string>();

            var categoryQry = from d in db.Categories
                              orderby d.Title
                              select d.Title;
            categoriesLst.AddRange(categoryQry.Distinct());
            ViewBag.postCategories = categoriesLst;

            return View(post);
        }
        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditAdmin([Bind(Include = "PostID,PostTitle,PostContent,AuthorName,Date,PostCategory,PostStatus,ImageUrl,Rating")] Post post,HttpPostedFileBase imgFile)
        {
            // This code for image save in database
            string fileName = Path.GetFileNameWithoutExtension(imgFile.FileName);
            string extension = Path.GetExtension(imgFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            post.ImageUrl = "/img/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/img/"), fileName);
            imgFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Display");
            }
            return View(post);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAdmin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }


        // POST: Posts/Delete/5
        [HttpPost, ActionName("DeleteAdmin")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmedAdmin(int id)
        {
            Post post = db.Posts.Find(id);
            Trash trash = new Trash();
            trash.AuthorName = post.AuthorName;
            trash.Date = post.Date;
            trash.ImageUrl = post.ImageUrl;
            trash.PostCategory = post.PostCategory;
            trash.TrashID = post.PostID;
            trash.PostTitle = post.PostTitle;
            trash.PostContent = post.PostContent;
            trash.PostStatus = post.PostStatus;
            db.Trashes.Add(trash);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Display");
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
