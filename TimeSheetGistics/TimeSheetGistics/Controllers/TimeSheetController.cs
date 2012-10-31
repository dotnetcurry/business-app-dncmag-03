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
    public class TimeSheetController : Controller
    {
        private TimeSheetGisticsContext db = new TimeSheetGisticsContext();

        //
        // GET: /TimeSheet/

        public ActionResult Index()
        {
            var timesheets = db.TimeSheets.Include(t => t.ParentProject)
                                            .Include(t => t.TimeCards).ToList<TimeSheet>();
            foreach (var timesheet in timesheets)
            {
                timesheet.TimeCards.Add(new
                TimeCard
                {
                    Title = "Time Card 1",
                    TaskDetails = "Time Card 1 Details",
                    ParentTimeSheet = timesheet,
                    TimeSheetId = timesheet.Id,
                    Hours = 8
                });
                timesheet.TimeCards.Add(new
                TimeCard
                {
                    Title = "Time Card 2",
                    TaskDetails = "Time Card 2 Details",
                    ParentTimeSheet = timesheet,
                    TimeSheetId = timesheet.Id,
                    Hours = 8
                });
                timesheet.TimeCards.Add(new
                TimeCard
                {
                    Title = "Time Card 3",
                    TaskDetails = "Time Card 3 Details",
                    ParentTimeSheet = timesheet,
                    TimeSheetId = timesheet.Id,
                    Hours = 8
                });
            }
            return View(timesheets.AsQueryable<TimeSheet>());

        }

        //
        // GET: /TimeSheet/Details/5

        public ActionResult Details(int id = 0)
        {
            TimeSheet timesheet = db.TimeSheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        //
        // GET: /TimeSheet/Create

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ReferenceNumber");
            return View();
        }

        //
        // POST: /TimeSheet/Create

        [HttpPost]
        public ActionResult Create(TimeSheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.TimeSheets.Add(timesheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ReferenceNumber", timesheet.ProjectId);
            return View(timesheet);
        }

        //
        // GET: /TimeSheet/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TimeSheet timesheet = db.TimeSheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ReferenceNumber", timesheet.ProjectId);
            return View(timesheet);
        }

        //
        // POST: /TimeSheet/Edit/5

        [HttpPost]
        public ActionResult Edit(TimeSheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "ReferenceNumber", timesheet.ProjectId);
            return View(timesheet);
        }

        //
        // GET: /TimeSheet/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TimeSheet timesheet = db.TimeSheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        //
        // POST: /TimeSheet/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSheet timesheet = db.TimeSheets.Find(id);
            db.TimeSheets.Remove(timesheet);
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