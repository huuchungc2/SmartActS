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
using Microsoft.AspNet.Identity.Owin;

namespace SmartActS.Controllers
{
    public class LocationsController : Controller
    {
        private SmartActSModel db = new SmartActSModel();
        private ApplicationUserManager _userManager;
        // GET: Locations
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var roles = UserManager.GetRoles(userId);
            var roleName = roles.First();
            var userid = User.Identity.GetUserId();
            ViewBag.IsAdmin = "no";
            ViewBag.IsCustomer = "no";
            ViewBag.IsSupply = "no";
            switch (roleName)
            {

                case "Customer":
                    ViewBag.IsCustomer = "yes";

                    return View(db.Locations.ToList());
                default:
                    {
                        ViewBag.IsAdmin = "yes";
                        return View(db.Locations.ToList());
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
        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
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


           // ViewBag.CategoryList = new SelectList(cat.GetCategoryList(), "CategoryId", "CategoryName");
            ViewBag.ListLocation = new SelectList( items, "Value", "Text");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationId,LocationCode,LocationName,PrarentId,LngTitude,LatTitude")] Location location, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int parent_Id = -1;
                //  category.CategoryId = Convert.ToInt32(FormCollection["Select Category"]);
                try
                {
                    parent_Id = int.Parse(form["ddParent"].ToString());
                }
                catch (Exception)
                {

                    parent_Id = -1;
                }
                if (parent_Id != -1)
                    location.ParentId = parent_Id;
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationId,LocationCode,LocationName,PrarentId,LngTitude,LatTitude")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
