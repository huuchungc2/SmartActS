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
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using SmartActS.Models;

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

          
            var roles = UserManager.GetRoles(User.Identity.GetUserId());
            var roleName = roles.First();
            var userId = User.Identity.GetUserId();
            ViewBag.IsAdmin = "no";
            ViewBag.IsCustomer = "no";
            ViewBag.IsSupply = "no";
            switch (roleName)
            {
                case "Customer":
                    var customer = db.Customers.Where(m => m.UserId ==userId).First();
                    var request = db.Requests.Where(m => m.CustomerId == customer.CustomerId);
                    var requestIds = (from d in request select d.RequestId);
                    ViewBag.IsCustomer = "yes";

                    return View(db.Responses.Where(m => requestIds.Contains(m.RequestId)).OrderByDescending(m=>m.PriceSuggest).OrderByDescending(m=>m.ResponseTime));

                case "Supply":
                    var supply = db.Supplies.Where(m => m.UserId == userId).First();
                    ViewBag.IsSupply = "yes";
                    return View(db.Responses.Where(m => m.SupplyId == supply.SupplyId).ToList().OrderByDescending(m => m.ResponseTime));
                case "Admin":
                    ViewBag.IsAdmin = "yes";
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

        //// GET: Responses/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}
        // GET: Responses/Create
        public ActionResult Create(int ? id)
        {
            Request request = db.Requests.Find(id);
            if (request != null) { 
            Response objResponse = new DataModels.Response();
            objResponse.categoryId = request.CategoryId;
            objResponse.RequestId = request.RequestId;
                objResponse.Status =(int) Common.eResponse.Pending;
            // Get current 
            var userid = User.Identity.GetUserId();
            objResponse.SupplyId = db.Supplies.Where(m => m.UserId == userid).First().SupplyId;
            
            return View(objResponse);
            }
            return View();
        }

     
        public ActionResult Confirm(int? id)
        {
            Response response = db.Responses.Find(id);
            if (response != null)
            {
                // get all response of this request will be rejected
                foreach (var item in db.Responses.Where(m => m.RequestId == response.RequestId))
                {
                    item.Status = (int)Common.eResponse.Rejected;
                    db.Entry(item).State = EntityState.Modified;
                   // db.SaveChanges();
                }
                // update status only this reponse is confirm
                 response.Status = (int)Common.eResponse.Confirmed;
                 db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
      
        public ActionResult Reject(int? id)
        {
            Response response = db.Responses.Find(id);
            if (response != null)
            {
               // update status only this reponse is confirm
                response.Status = (int)Common.eResponse.Rejected;
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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
                response.ResponseTime = DateTime.Now;
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
