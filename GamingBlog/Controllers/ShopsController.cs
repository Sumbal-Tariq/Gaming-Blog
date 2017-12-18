using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingBlog.Models;
using PagedList;
using Microsoft.AspNet.Identity;

namespace GamingBlog.Controllers
{
    public class ShopsController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Shops
        public ActionResult Index(string productCategory, string SearchString, int? page)
        {
            var categoryLst = new List<string>();

            var categoryQry = from d in db.Products
                              orderby d.ProductCategory
                              select d.ProductCategory;

            categoryLst.AddRange(categoryQry.Distinct());
            ViewBag.products = categoryLst;

            var products = from m in db.Products
                        select m;
            // use only those post for display with status of published
            products = products.Where(s => s.ProductStatus.Equals("Published"));

            if (!String.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.ProductName.Contains(SearchString));
                products = products.Where(s => s.ProductStatus.Equals("Published"));
            }

            if (!string.IsNullOrEmpty(productCategory))
            {
                products = products.Where(x => x.ProductCategory == productCategory);
            }
            products = products.OrderByDescending(m => m.ProductPrice);
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Shops/Details/5
        public ActionResult Details(int? id)
        {
            Product product = db.Products.Find(id);
            var userId = User.Identity.GetUserId();
            var allitems = db.Carts.Where(d => d.userID == userId).Count();
            ViewBag.item = allitems;
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleId = id.Value;
            return View(product);
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
