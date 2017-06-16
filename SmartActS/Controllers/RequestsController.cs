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

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

namespace SmartActS.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
       // private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        Models.ApplicationDbContext context = new Models.ApplicationDbContext();
        private SmartActSModel db = new SmartActSModel();

        // GET: Requests
        public ActionResult Index()
        {
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
                    return View(db.Requests.Where(m => m.CustomerId == customer.CustomerId).ToList().OrderByDescending(m => m.CreatedDate));

                case "Supply":
                    ViewBag.IsSupply = "yes";
                    var supply = db.Supplies.Where(m => m.UserId == userid).First();
                    return View(db.Requests.Where(m => m.CategoryId == supply.CategoryId).ToList().OrderByDescending(m=>m.CreatedDate));
                   
                   
                case "Admin":
                    ViewBag.IsAdmin = "yes";
                    return View(db.Requests.ToList());
                default: return RedirectToAction("index", "Manage");
            }

            // all request of current user
           
           
           
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
        
        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()

        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in db.Locations.ToList())
            {
                SelectListItem sitem = new SelectListItem();
                sitem.Value = item.LocationId.ToString();
                sitem.Text = item.LocationName;
                items.Add(sitem);

            }
            ViewBag.ListLocation = new SelectList(items, "Value", "Text");
            List<SelectListItem> itemCat = new List<SelectListItem>();
            foreach (var item in db.Categories.ToList())
            {
                SelectListItem sitemCate = new SelectListItem();
                sitemCate.Value = item.CategoryId.ToString();
                sitemCate.Text = item.CategoryName;
                itemCat.Add(sitemCate);

            }

            // ViewBag.CategoryList = new SelectList(cat.GetCategoryList(), "CategoryId", "CategoryName");
            ViewBag.ListCategory = new SelectList(itemCat, "Value", "Text");

           // ViewBag.ListLocation = new SelectList(items, "Value", "Text");
            List<SelectListItem> itemDu = new List<SelectListItem>();

            SelectListItem sitemDu = new SelectListItem();
                sitemDu.Value = "h";
                sitemDu.Text = "Hour";
                itemDu.Add(sitemDu);
                sitemDu = new SelectListItem();
                 sitemDu.Value = "d";
                sitemDu.Text = "Day";
            itemDu.Add(sitemDu);

            // ViewBag.CategoryList = new SelectList(cat.GetCategoryList(), "CategoryId", "CategoryName");
            ViewBag.ListDuration = new SelectList(itemDu, "Value", "Text");

            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestId,RequestCode,CategoryId,CustomerId,Status,CreatedDate,DurationExpired,FromBudget,ToBudget,RequireResponse,ShippingAddress,LocationSupplyId,Description,BestTime,BestSupply,BestPrice,RequestTitle,FileAttrachId")] Request request, System.Web.Mvc.FormCollection form)
        {
            if (ModelState.IsValid)
            {
                request.CreatedDate = DateTime.Now;
                int cat_id = -1;
                //  category.CategoryId = Convert.ToInt32(FormCollection["Select Category"]);
                try
                {
                    cat_id = int.Parse(form["ddCategory"].ToString());
                }
                catch (Exception)
                {

                    cat_id = -1;
                }
                if (cat_id != -1)
                    request.CategoryId = cat_id;
                int locat_id = -1;

                //  category.CategoryId = Convert.ToInt32(FormCollection["Select Category"]);
                try
                {
                    locat_id = int.Parse(form["ddLocation"].ToString());
                }
                catch (Exception)
                {

                    locat_id = -1;
                }
                if (locat_id != -1)
                    request.LocationSupplyId = locat_id;
                int durat_ex = 0;
                //  category.CategoryId = Convert.ToInt32(FormCollection["Select Category"]);
                //change to hour to store into database
                try
                {
                   // locat_id = int.Parse(form["ddLocation"].ToString());
                    if(form["ddDuration"].ToString()=="d")
                    {
                        durat_ex = 24 * int.Parse(request.DurationExpired.ToString());
                    }
                }
                catch (Exception)
                {

                    durat_ex = 0;
                }
                var userId = User.Identity.GetUserId();
                var customer = db.Customers.Where(m => m.UserId == userId).First();

                request.Status = 0;// 0 pendding, 1 approval, 2 expired
                request.CustomerId = customer.CustomerId;
                request.DurationExpired = durat_ex;
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            if (request.CategoryId>0)
            {
                var selected = (from sub in db.Categories
                                where sub.CategoryId == request.CategoryId
                                select sub.CategoryId).First();
                ViewBag.ListCategory = new SelectList(db.Categories, "CategoryId", "CategoryName",selected);
                //ViewData["ListCategory"] = new SelectList(db.Categories, "CategoryId", "CategoryName", selected);
            }
            else
            {
                ViewBag.ListCategory = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
               // ViewData["ListCategory"] = new SelectList(db.Categories, "CategoryId", "CategoryName");
            }
            if (request.LocationSupplyId != null)
            {
                var selected = (from sub in db.Locations
                                where sub.LocationId == request.LocationSupplyId
                                select sub.LocationId).First();
                  ViewBag.ListLocation = new SelectList(db.Locations.ToList(), "LocationId", "LocationName",selected);
               // ViewData["ListLocation"] = new SelectList(db.Locations.ToList(), "LocationId", "LocationName", selected);
            }
            else
            {
                //ViewData["ListLocation"] = new SelectList(db.Locations.ToList(), "LocationId", "LocationName");
                ViewBag.ListLocation = new SelectList(db.Locations, "LocationId", "LocationName");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestId,RequestCode,CategoryId,CustomerId,Status,CreatedDate,DurationExpired,FromBudget,ToBudget,RequireResponse,ShippingAddress,LocationSupplyId,Description,BestTime,BestSupply,BestPrice,RequestTitle,FileAttrachId")] Request request, System.Web.Mvc.FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int cat_id = -1;
                //  category.CategoryId = Convert.ToInt32(FormCollection["Select Category"]);
                try
                {
                    cat_id = int.Parse(form["ddCategory"].ToString());
                }
                catch (Exception)
                {

                    cat_id = -1;
                }
                if (cat_id != -1)
                    request.CategoryId = cat_id;
                int locat_id = -1;

                //  category.CategoryId = Convert.ToInt32(FormCollection["Select Category"]);
                try
                {
                    locat_id = int.Parse(form["ddLocation"].ToString());
                }
                catch (Exception)
                {

                    locat_id = -1;
                }
                if (locat_id != -1)
                    request.LocationSupplyId = locat_id;
                
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
