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
    public class CustomersController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Customers/Create
        public ActionResult Create()
        {
            var userID = User.Identity.GetUserId();
            var customer = db.Customers.Where(d => d.userId == userID).ToList();
            if(customer.Count == 1)
            {
                var cust = db.Customers.Single(d => d.userId == userID);
                Edit(cust.CustomerID);
            }
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CreditCard,userEmail,userName,userId,phn_No")] Customer customer)
        {
            var userID = User.Identity.GetUserId();
            var customerr = db.Customers.Where(d => d.userId == userID).ToList();
            if (customerr.Count == 1)
            {
                var cust = db.Customers.Single(d => d.userId == userID);
                return RedirectToAction("Edit", "Customers", new { id = cust.CustomerID });
            }
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Create","Addresses");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CreditCard,userEmail,userName,userId,phn_No")] Customer customer)
        {
            Customer customerr = db.Customers.Find(customer.CustomerID);
            if (ModelState.IsValid)
            {
                db.Entry(customerr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit","Addresses", new { id = customerr.address });
            }
            return View(customer);
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
