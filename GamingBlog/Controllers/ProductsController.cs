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

namespace GamingBlog.Controllers
{
    public class ProductsController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Products
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 15;
            var products = from s in db.Products select s;
            products = products.OrderByDescending(s => s.ProductPrice);
            ViewBag.TotalProducts = products.Count();
            ViewBag.Published = products.Where(s => s.ProductStatus.Equals("Published"));
            ViewBag.PublishedProducts = products.Where(s => s.ProductStatus.Equals("Published")).Count();
            ViewBag.Draft = products.Where(s => s.ProductStatus.Equals("Draft"));
            ViewBag.DraftProducts = products.Where(s => s.ProductStatus.Equals("Draft")).Count();
            ViewBag.Draft = products.Where(s => s.ProductStatus.Equals("Draft"));
            ViewBag.DraftProducts = products.Where(s => s.ProductStatus.Equals("Draft")).Count();
            ViewBag.Trash = products.Where(s => s.ProductStatus.Equals("Trash"));
            ViewBag.TrashProducts = products.Where(s => s.ProductStatus.Equals("Trash")).Count();
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            // This code is for the categories drop down menu
            var categoriesLst = new List<string>();

            var categoryQry = from d in db.PCategories
                              orderby d.PCategoryTitle
                              select d.PCategoryTitle;
            categoriesLst.AddRange(categoryQry.Distinct());
            ViewBag.productCategories = categoriesLst;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ProductPrice,ProductDescription,itemStatus,quantity,ProductCategory,ProductStatus,ImageUrl")] Product product, HttpPostedFileBase imFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(imFile.FileName);
            string extension = Path.GetExtension(imFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            product.ImageUrl = "/img/Products/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/img/Products/"), fileName);
            imFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // This code is for the categories drop down menu
            var categoriesLst = new List<string>();

            var categoryQry = from d in db.PCategories
                              orderby d.PCategoryTitle
                              select d.PCategoryTitle;
            categoriesLst.AddRange(categoryQry.Distinct());
            ViewBag.productCategories = categoriesLst;
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            // This code is for the categories drop down menu
            var categoriesLst = new List<string>();

            var categoryQry = from d in db.PCategories
                              orderby d.PCategoryTitle
                              select d.PCategoryTitle;
            categoriesLst.AddRange(categoryQry.Distinct());
            ViewBag.productCategories = categoriesLst;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ProductPrice,ProductDescription,itemStatus,quantity,ProductCategory,ProductStatus,ImageUrl")] Product product,HttpPostedFileBase imFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(imFile.FileName);
            string extension = Path.GetExtension(imFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            product.ImageUrl = "/img/Products/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/img/Products"), fileName);
            imFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var categoriesLst = new List<string>();

            var categoryQry = from d in db.PCategories
                              orderby d.PCategoryTitle
                              select d.PCategoryTitle;
            categoriesLst.AddRange(categoryQry.Distinct());
            ViewBag.productCategories = categoriesLst;
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Trash(int id)
        {
            Product product = db.Products.Find(id);
            if (product.ProductStatus == "Draft")
            {
                product.TrashStatus = product.ProductStatus;
                product.ProductStatus = "Trash";

            }
            else
            {
                product.TrashStatus = product.ProductStatus;
                product.ProductStatus = "Trash";
            }
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Restore(int id)
        {
            Product product = db.Products.Find(id);
            if (product.ProductStatus == "Draft")
            {
                product.ProductStatus = "Draft";
            }
            else
            {
                product.ProductStatus = "Published";
            }
            db.Entry(product).State = EntityState.Modified;
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
