using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartActS.DataModels;

namespace SmartActS.Controllers
{
    public class RequestsController : Controller
    {
        private SmartActSModel db = new SmartActSModel();

        // GET: Requests
        public ActionResult Index()
        {
            ViewBag.ListLocaton = db.Locations.ToList();
            ViewBag.ListCategory = db.Locations.ToList();
            return View(db.Requests.ToList());
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
        public ActionResult Create([Bind(Include = "RequestId,RequestCode,CategoryId,CustomerId,Status,CreatedDate,DurationExpired,FromBudget,ToBudget,RequireResponse,ShippingAddress,LocationSupplyId,Description,BestTime,BestSupply,BestPrice,RequestTitle,FileAttrachId")] Request request, FormCollection form)
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
                request.Status = 0;// 0 pendding, 1 approval, 2 expired
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

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestId,RequestCode,CategoryId,CustomerId,Status,CreatedDate,DurationExpired,FromBudget,ToBudget,RequireResponse,ShippingAddress,LocationSupplyId,Description,BestTime,BestSupply,BestPrice,RequestTitle,FileAttrachId")] Request request)
        {
            if (ModelState.IsValid)
            {
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
