using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartActS.DataModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace SmartActS.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private ApplicationUserManager _userManager;
        Models.ApplicationDbContext context = new Models.ApplicationDbContext();
        private SmartActSModel db = new SmartActSModel();

        // GET: Customers
        public ActionResult Index()
        {
            // return View(db.Customers.ToList());
            //  return View(db.Supplies.ToList());
            //ViewBag.ListLocaton = db.Locations.ToList();
            //ViewBag.ListCategory = db.Locations.ToList();
            var roles = UserManager.GetRoles(User.Identity.GetUserId());
            var roleName = roles.First();
            var userid = User.Identity.GetUserId();
            ViewBag.IsAdmin = "no";
            ViewBag.IsCustomer = "no";
            ViewBag.IsSupply = "no";
            switch (roleName)
            {

                case "Customer":
                    ViewBag.IsCustomer = "yes";
                    var customer = db.Customers.Where(m => m.UserId == userid).First();
                    return View(db.Customers.Where(m => m.CustomerId == customer.CustomerId).ToList());
                default:
                    {
                        ViewBag.IsAdmin = "yes";
                        return View(db.Customers.ToList());
                    }
                 
               // default: return RedirectToAction("index", "Manage");
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
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

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,CustomerCode,CustomerName,Email,MobiPhone,Address,LngTitude,LatTitude,LocationId,IsStatus,UserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include = "CustomerId,CustomerCode,CustomerName,Email,MobiPhone,Address,LngTitude,LatTitude,LocationId,IsStatus,UserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
