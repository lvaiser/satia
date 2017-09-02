﻿using LoginPoC.DAL;
using LoginPoC.Model.ProcessType;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LoginPoC.Model.User;

namespace LoginPoC.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Agent + ", " + ApplicationUserRoles.Administrator)]
    public class ProcessTypeController : Controller
    {
        // GET DbContext from container
        private ApplicationDbContext db;

        public ProcessTypeController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: ProcessType
        [OverrideAuthorization]
        [Authorize]
        public ActionResult Index()
        {
            return View(db.ProcessTypes.ToList());
        }

        // GET: ProcessType/Details/5
        [OverrideAuthorization]
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessType processType = db.ProcessTypes.Find(id);
            if (processType == null)
            {
                return HttpNotFound();
            }
            return View(processType);
        }

        // GET: ProcessType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProcessType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] ProcessType processType)
        {
            if (ModelState.IsValid)
            {
                db.ProcessTypes.Add(processType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(processType);
        }

        // GET: ProcessType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessType processType = db.ProcessTypes.Find(id);
            if (processType == null)
            {
                return HttpNotFound();
            }
            return View(processType);
        }

        // POST: ProcessType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] ProcessType processType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(processType);
        }

        // GET: ProcessType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessType processType = db.ProcessTypes.Find(id);
            if (processType == null)
            {
                return HttpNotFound();
            }
            return View(processType);
        }

        // POST: ProcessType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProcessType processType = db.ProcessTypes.Find(id);
            db.ProcessTypes.Remove(processType);
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
