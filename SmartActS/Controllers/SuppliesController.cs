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
    [Authorize(Roles = "Supply")]
    public class SuppliesController : Controller
    {
        private ApplicationUserManager _userManager;
        Models.ApplicationDbContext context = new Models.ApplicationDbContext();
        private SmartActSModel db = new SmartActSModel();

        // GET: Supplies
        public ActionResult Index()
        {
            //  return View(db.Supplies.ToList());
            //ViewBag.ListLocaton = db.Locations.ToList();
            //ViewBag.ListCategory = db.Locations.ToList();
            var roles = UserManager.GetRoles(User.Identity.GetUserId());
            var roleName = roles.First();
            var userid = User.Identity.GetUserId();
            switch (roleName)
            {
               case "Supply":
                    var supply = db.Supplies.Where(m => m.UserId == userid).First();
                    return View(db.Supplies.Where(m => m.SupplyId == supply.SupplyId).ToList());
                default:
                   return View(db.Supplies.ToList());
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

        // GET: Supplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // GET: Supplies/Create
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
            return View();
        }

        // POST: Supplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplyId,SupplyCode,SupplyName,LocationId,LngTitude,Latitude,CategoryId,Address,Email,MobiPhone,Phone,Website,RankId,IsStatus,UserId")] Supply supply)
        {
            
            if (ModelState.IsValid)
            {
                db.Supplies.Add(supply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supply);
        }

        // GET: Supplies/Edit/5
        public ActionResult Edit(int? id)
        {
            //List<SelectListItem> items = new List<SelectListItem>();
            //foreach (var item in db.Locations.ToList())
            //{
            //    SelectListItem sitem = new SelectListItem();
            //    sitem.Value = item.LocationId.ToString();
            //    sitem.Text = item.LocationName;
            //    items.Add(sitem);

            //}
           
           // List<SelectListItem> itemCat = new List<SelectListItem>();
            //foreach (var item in db.Categories.ToList())
            //{
            //    SelectListItem sitemCate = new SelectListItem();
            //    sitemCate.Value = item.CategoryId.ToString();
            //    sitemCate.Text = item.CategoryName;
            //    itemCat.Add(sitemCate);

            //}
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            if(supply.CategoryId !=null)
            { 
            var selected = (from sub in db.Categories
                            where sub.CategoryId ==supply.CategoryId
                                 select sub.CategoryId).First();
            ViewBag.ListCategory = new SelectList(db.Categories, "CategoryId", "CategoryName", selected);
            }else
            {
                ViewBag.ListCategory = new SelectList(db.Categories, "CategoryId", "CategoryName", 1);
            }
            if (supply.LocationId != null)
            {
                var selected = (from sub in db.Locations
                                where sub.LocationId == supply.LocationId
                                select sub.LocationId).First();
                ViewBag.ListLocation = new SelectList(db.Locations, "LocationId", "LocationName", selected);
            }
            else
            {
                ViewBag.ListLocation = new SelectList(db.Locations, "LocationId", "LocationName", 1);
            }
           // ViewBag.ListLocation = new SelectList(items, "Value", "Text");
            // ViewBag.CategoryList = new SelectList(cat.GetCategoryList(), "CategoryId", "CategoryName");
            //  ViewBag.ListCategory = new SelectList(db.Categories, "Value", "Text", selected);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
             
         
           // ViewBag.ListCategory = new SelectList(itemCat, "Value", "Text",);
            return View(supply);
        }

        // POST: Supplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplyId,SupplyCode,SupplyName,LocationId,LngTitude,Latitude,CategoryId,Address,Email,MobiPhone,Phone,Website,RankId,IsStatus,UserId")] Supply supply,FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supply).State = EntityState.Modified;
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
                    supply.CategoryId = cat_id;
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
                    supply.LocationId = locat_id;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supply);
        }

        // GET: Supplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supply supply = db.Supplies.Find(id);
            db.Supplies.Remove(supply);
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
