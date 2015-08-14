using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MyFollow.DAL;
using MyFollow.Models;

namespace MyFollow.Controllers
{
    public class ProductOwnersController : Controller
    {
        private MyFollowContext db = new MyFollowContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: ProductOwners
        public ActionResult Index()
        {
            return View(db.ProductOwners.ToList());
        }

        // GET: ProductOwners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOwner productOwner = db.ProductOwners.Find(id);
            if (productOwner == null)
            {
                return HttpNotFound();
            }
            return View(productOwner);
        }

        // GET: ProductOwners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyName,EmailId,Description,DateofJoin,FoundedIn,Street1,Street2,City,State,Country,Pincode,ContactNumber,Website,Twitter,Facebook,Password,ApprovalFlag")] ProductOwner productOwner)
        {
            if (ModelState.IsValid)
            {
                db.ProductOwners.Add(productOwner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productOwner);
        }

        // GET: ProductOwners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOwner productOwner = db.ProductOwners.Find(id);
            if (productOwner == null)
            {
                return HttpNotFound();
            }
            return View(productOwner);
        }

        // POST: ProductOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,EmailId,Description,DateofJoin,FoundedIn,Street1,Street2,City,State,Country,Pincode,ContactNumber,Website,Twitter,Facebook,Password,ApprovalFlag")] ProductOwner productOwner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productOwner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productOwner);
        }

        // GET: ProductOwners/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProductOwner productOwner = db.ProductOwners.Find(id);
        //    if (productOwner == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(productOwner);
        //}

        //// POST: ProductOwners/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ProductOwner productOwner = db.ProductOwners.Find(id);
        //    db.ProductOwners.Remove(productOwner);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
