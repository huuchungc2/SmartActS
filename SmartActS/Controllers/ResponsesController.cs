using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartActS.DataModels;
using Microsoft.AspNet.Identity;

namespace SmartActS.Controllers
{
    [Authorize]
    public class ResponsesController : Controller
    {
        private SmartActSModel db = new SmartActSModel();
        private ApplicationUserManager _userManager;
        Models.ApplicationDbContext context = new Models.ApplicationDbContext();
        // GET: Responses
        public ActionResult Index()
        {

            //// all request of current user
            //var userId = User.Identity.GetUserId();
            //var supply = db.Supplies.Where(m => m.UserId == userId).First();

            //return View(db.Responses.Where(m => m.SupplyId == supply.SupplyId).ToList());
            //  return View(db.Responses.ToList());
            //ViewBag.ListLocaton = db.Locations.ToList();
            //ViewBag.ListCategory = db.Locations.ToList();
            var roles = UserManager.GetRoles(User.Identity.GetUserId());
            var roleName = roles.First();
            switch (roleName)
            {
                case "Customer":
                    var customer = db.Customers.Where(m => m.UserId == User.Identity.GetUserId()).First();
                    var request = db.Requests.Where(m => m.CustomerId == customer.CustomerId);
                    var requestIds = (from d in request select d.RequestId);


                    return View(db.Responses.Where(m => requestIds.Contains(m.RequestId)));

                case "Supply":
                    var supply = db.Supplies.Where(m => m.UserId == User.Identity.GetUserId()).First();
                    return View(db.Responses.Where(m => m.SupplyId == supply.SupplyId).ToList().OrderByDescending(m => m.ResponseTime));
                case "Admin":
                    return View(db.Responses.ToList());
                default: return RedirectToAction("index", "Manage");
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
        // GET: Responses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // GET: Responses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Responses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResponseId,ResponseTitle,RequestId,SupplyId,ResponseTime,Status,PriceSuggest,Description,FileAttachId,categoryId")] Response response)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var supply = db.Supplies.Where(m => m.UserId == userId).First();
                response.SupplyId = supply.SupplyId;
                db.Responses.Add(response);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(response);
        }

        // GET: Responses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResponseId,ResponseTitle,RequestId,SupplyId,ResponseTime,Status,PriceSuggest,Description,FileAttachId,categoryId")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(response);
        }

        // GET: Responses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: Responses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Response response = db.Responses.Find(id);
            db.Responses.Remove(response);
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
