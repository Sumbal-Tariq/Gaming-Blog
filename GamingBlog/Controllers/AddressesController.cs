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
    public class AddressesController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Addresses
        public ActionResult Index()
        {
            return View(db.Addresses.ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            var customer = db.Customers.Where(d => d.userId == address.userid).ToList();
            var cart = db.Carts.Where(d => d.userID == address.userid).ToList();
            ViewBag.cust = customer;
            ViewBag.carts = cart;
            var allitems = db.Carts.Where(d => d.userID == address.userid).ToList();
            ViewBag.items = allitems;
            if (allitems.Count() > 0)
            {
                var itemsSum = allitems.Sum(d => d.totalCost);
                ViewBag.itemSum = itemsSum;
            }
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ZipCode,City,Country,userid,_Address")] Address addresss)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(addresss);
                db.SaveChanges();
                var customer = db.Customers.Single(d => d.userId == addresss.userid);
                customer.address = addresss.id;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(addresss);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }


        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ZipCode,City,Country,userid,_Address")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" +address.id);
            }
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            var cart = db.Carts.Where(d => d.userID == address.userid).ToList();
            for (int i = 0; i < cart.Count;i++)
            {
                var cartt = db.Carts.Single(d => d.userID == address.userid);
                db.Carts.Remove(cartt);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Shops");
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
