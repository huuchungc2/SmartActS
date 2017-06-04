﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartActS.DataModels;


namespace SmartActS.Controllers
{
    public class RequestController : Controller
    {
        SmartActS.DataModels.SmartActSModel _db;

        public RequestController()
        {
            _db = new DataModels.SmartActSModel();
        }
        // GET: Request
        public ActionResult Index()
        {
            ViewData.Model = _db.Requests.ToList();
            return View();
        }

        // GET: Request/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Request/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Request/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var RequestAdd = new Request();
                TryUpdateModel(RequestAdd, collection);
                _db.Requests.Add(RequestAdd);
                _db.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Request/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Request/Edit/5
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

        // GET: Request/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Request/Delete/5
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
