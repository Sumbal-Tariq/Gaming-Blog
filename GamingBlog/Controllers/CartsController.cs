using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingBlog.Models;
using Microsoft.AspNet.Identity;

namespace GamingBlog.Controllers
{
    public class CartsController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Carts
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var allitems = db.Carts.Where(d => d.userID == userId).ToList();
            ViewBag.items = allitems;
            ViewBag.itemcount = allitems.Count;
            if (allitems.Count() > 0)
            {
                var itemsSum = allitems.Sum(d => d.totalCost);
                ViewBag.itemSum = itemsSum;
            }
                return View();
        }


        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CartID,cartItemName,itemID,itemQuantity,userID,UnitCost,totalCost,itemImage")] Cart cart)
        {
            var product = db.Products.Single(d => d.ProductID.Equals(cart.itemid));
            product.quantity = product.quantity - cart.itemQuantity;
            if(product.quantity == 0)
            {
                product.itemStatus = "NotAvailable";
            }
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            var item = db.Carts.Where(d => d.itemid == cart.itemid && d.userID == cart.userID).ToList();
            if (item.Count == 1)
            {
                if (ModelState.IsValid)
                {
                    var id = cart.itemid;
                    var cartt = db.Carts.Single(d => d.itemid.Equals(id));
                    cartt.itemQuantity = cartt.itemQuantity + cart.itemQuantity;
                    cartt.totalCost = cartt.itemQuantity * cartt.UnitCost;
                    db.Entry(cartt).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details/" + cart.itemid, "Shops");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    cart.totalCost = cart.itemQuantity * cart.UnitCost;
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    return RedirectToAction("Details/"+cart.itemid,"Shops");
                }
            }
            return View(cart);
        }

        //// GET: Carts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cart cart = db.Carts.Find(id);
        //    if (cart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cart);
        //}

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            var product = db.Products.Single(d => d.ProductID.Equals(cart.itemid));
            product.quantity = product.quantity + cart.itemQuantity;
            if (product.quantity == 0)
            {
                product.itemStatus = "NotAvailable";
            }
            else
            {
                product.itemStatus = "Available";
            }
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            db.Carts.Remove(cart);
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
