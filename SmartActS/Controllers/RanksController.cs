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
    public class RanksController : Controller
    {
        private SmartActSModel db = new SmartActSModel();
        private ApplicationUserManager _userManager;
        // GET: Ranks
        public ActionResult Index()
        {
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

                    return View(db.Ranks.ToList());
                default:
                    {
                        ViewBag.IsAdmin = "yes";
                        return View(db.Ranks.ToList());
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
        // GET: Ranks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rank rank = db.Ranks.Find(id);
            if (rank == null)
            {
                return HttpNotFound();
            }
            return View(rank);
        }

        // GET: Ranks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ranks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RankId,RankCode,RankName")] Rank rank)
        {
            if (ModelState.IsValid)
            {
                db.Ranks.Add(rank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rank);
        }

        // GET: Ranks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rank rank = db.Ranks.Find(id);
            if (rank == null)
            {
                return HttpNotFound();
            }
            return View(rank);
        }

        // POST: Ranks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RankId,RankCode,RankName")] Rank rank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rank);
        }

        // GET: Ranks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rank rank = db.Ranks.Find(id);
            if (rank == null)
            {
                return HttpNotFound();
            }
            return View(rank);
        }

        // POST: Ranks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rank rank = db.Ranks.Find(id);
            db.Ranks.Remove(rank);
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
