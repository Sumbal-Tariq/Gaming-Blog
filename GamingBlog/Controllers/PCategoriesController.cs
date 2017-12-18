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
    public class PCategoriesController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: PCategories
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.PCategories.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: PCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PCategory pCategory = db.PCategories.Find(id);
            if (pCategory == null)
            {
                return HttpNotFound();
            }
            var products = from m in db.Products
                        select m;
            // use only those post for display with status of published
            products = products.Where(s => s.ProductCategory.Equals(pCategory.PCategoryTitle));
            ViewBag.product = products;
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: PCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "PCategoryID,PCategoryTitle")] PCategory pCategory)
        {
            if (ModelState.IsValid)
            {
                db.PCategories.Add(pCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pCategory);
        }

        [Authorize(Roles = "Admin")]
        // GET: PCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PCategory pCategory = db.PCategories.Find(id);
            if (pCategory == null)
            {
                return HttpNotFound();
            }
            return View(pCategory);
        }

        // POST: PCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "PCategoryID,PCategoryTitle")] PCategory pCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pCategory);
        }

        // GET: PCategories/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PCategory pCategory = db.PCategories.Find(id);
            if (pCategory == null)
            {
                return HttpNotFound();
            }
            return View(pCategory);
        }

        // POST: PCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PCategory pCategory = db.PCategories.Find(id);
            db.PCategories.Remove(pCategory);
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
