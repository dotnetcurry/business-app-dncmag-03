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
    public class TimeCardController : Controller
    {
        private TimeSheetGisticsContext db = new TimeSheetGisticsContext();

        //
        // GET: /TimeCard/

        public ActionResult Index()
        {
            var timecards = db.TimeCards.Include(t => t.ParentTimeSheet);
            return View(timecards.ToList());
        }

        //
        // GET: /TimeCard/Details/5

        public ActionResult Details(int id = 0)
        {
            TimeCard timecard = db.TimeCards.Find(id);
            if (timecard == null)
            {
                return HttpNotFound();
            }
            return View(timecard);
        }

        //
        // GET: /TimeCard/Create

        public ActionResult Create()
        {
            ViewBag.TimeSheetId = new SelectList(db.TimeSheets, "Id", "Id");
            return View();
        }

        //
        // POST: /TimeCard/Create

        [HttpPost]
        public ActionResult Create(TimeCard timecard)
        {
            if (ModelState.IsValid)
            {
                db.TimeCards.Add(timecard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TimeSheetId = new SelectList(db.TimeSheets, "Id", "Id", timecard.TimeSheetId);
            return View(timecard);
        }

        //
        // GET: /TimeCard/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TimeCard timecard = db.TimeCards.Find(id);
            if (timecard == null)
            {
                return HttpNotFound();
            }
            ViewBag.TimeSheetId = new SelectList(db.TimeSheets, "Id", "Id", timecard.TimeSheetId);
            return View(timecard);
        }

        //
        // POST: /TimeCard/Edit/5

        [HttpPost]
        public ActionResult Edit(TimeCard timecard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timecard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TimeSheetId = new SelectList(db.TimeSheets, "Id", "Id", timecard.TimeSheetId);
            return View(timecard);
        }

        //
        // GET: /TimeCard/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TimeCard timecard = db.TimeCards.Find(id);
            if (timecard == null)
            {
                return HttpNotFound();
            }
            return View(timecard);
        }

        //
        // POST: /TimeCard/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeCard timecard = db.TimeCards.Find(id);
            db.TimeCards.Remove(timecard);
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