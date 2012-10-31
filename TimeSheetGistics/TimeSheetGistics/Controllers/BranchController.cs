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
    public class BranchController : Controller
    {
        private TimeSheetGisticsContext db = new TimeSheetGisticsContext();

        //
        // GET: /Branch/

        public ActionResult Index()
        {
            var branches = db.Branches.Include(b => b.Location).Include(b => b.ParentCompany);
            return View(branches.ToList());
        }

        //
        // GET: /Branch/Details/5

        public ActionResult Details(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // GET: /Branch/Create

        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Zip");
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            return View();
        }

        //
        // POST: /Branch/Create

        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Zip", branch.LocationId);
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", branch.CompanyId);
            return View(branch);
        }

        //
        // GET: /Branch/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Zip", branch.LocationId);
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", branch.CompanyId);
            return View(branch);
        }

        //
        // POST: /Branch/Edit/5

        [HttpPost]
        public ActionResult Edit(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Zip", branch.LocationId);
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", branch.CompanyId);
            return View(branch);
        }

        //
        // GET: /Branch/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // POST: /Branch/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
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