using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartActS.Models;
using SmartActS.DataModels;
namespace SmartActS.Controllers
{
    public class CategoryController : Controller
    {
        SmartActS.DataModels.SmartActSModel _db;

        public CategoryController()
        {
            _db = new DataModels.SmartActSModel();
        }
        // GET: Category
        public ActionResult Index()
        {
            //ViewData.Model = _db.Categories.


            Category cat = new Category();
            cat.CategoryList = new SelectList(_db.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
            
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var CategoryAdd = new Category();
                TryUpdateModel(CategoryAdd);
                _db.Categories.Add(CategoryAdd);

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
