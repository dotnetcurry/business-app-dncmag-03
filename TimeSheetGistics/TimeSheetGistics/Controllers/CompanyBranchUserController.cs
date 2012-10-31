using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheetGistics.Data;
using TimeSheetGistics.Models;

namespace TimeSheetGistics.Controllers
{
    public class CompanyBranchUserController : Controller
    {
        private TimeSheetGisticsContext db = new TimeSheetGisticsContext();

        //
        // GET: /CompanyBranchUser/

        public ActionResult Index()
        {
            var companybranchusers = db.CompanyBranchUsers.Include(c => c.ParentBranch);
            return View(companybranchusers.ToList());
        }

        //
        // GET: /CompanyBranchUser/Details/5

        public ActionResult Details(int id = 0)
        {
            CompanyBranchUser companybranchuser = db.CompanyBranchUsers.Find(id);
            if (companybranchuser == null)
            {
                return HttpNotFound();
            }
            return View(companybranchuser);
        }

        //
        // GET: /CompanyBranchUser/Create

        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Description");
            return View();
        }

        //
        // POST: /CompanyBranchUser/Create

        [HttpPost]
        public ActionResult Create(CompanyBranchUser companybranchuser)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBranchUsers.Add(companybranchuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Description", companybranchuser.BranchId);
            return View(companybranchuser);
        }

        //
        // GET: /CompanyBranchUser/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CompanyBranchUser companybranchuser = db.CompanyBranchUsers.Find(id);
            if (companybranchuser == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Description", companybranchuser.BranchId);
            return View(companybranchuser);
        }

        //
        // POST: /CompanyBranchUser/Edit/5

        [HttpPost]
        public ActionResult Edit(CompanyBranchUser companybranchuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companybranchuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Description", companybranchuser.BranchId);
            return View(companybranchuser);
        }

        //
        // GET: /CompanyBranchUser/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CompanyBranchUser companybranchuser = db.CompanyBranchUsers.Find(id);
            if (companybranchuser == null)
            {
                return HttpNotFound();
            }
            return View(companybranchuser);
        }

        //
        // POST: /CompanyBranchUser/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyBranchUser companybranchuser = db.CompanyBranchUsers.Find(id);
            db.CompanyBranchUsers.Remove(companybranchuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}