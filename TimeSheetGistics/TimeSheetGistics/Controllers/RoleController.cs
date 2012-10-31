using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TimeSheetGistics.Models;
using Infragistics.Web.Mvc;

namespace TimeSheetGistics.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/

        public ActionResult Index()
        {
            string[] roles = Roles.GetAllRoles();
            int i = 0;
            IQueryable<RoleModel> rolesList = (from role in roles
                                         select new RoleModel { 
                                             Id= ++i,
                                             RoleName = role,
                                             RoleId = role
                                         }).AsQueryable<RoleModel>();
            return View(rolesList);
        }

        //
        // GET: /Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // POST: /Role/Create

        [ActionName("Create")]
        public ActionResult Create()
        {
            try
            {
                GridModel m = new GridModel();
                List<Transaction<RoleModel>> transactions = m.LoadTransactions<RoleModel>(HttpContext.Request.Form["ig_transactions"]);

                foreach (Transaction<RoleModel> t in transactions)
                {
                    if (t.type == "newrow")
                    {
                        Roles.CreateRole(t.row.RoleName);
                    }
                    else if (t.type == "deleterow")
                    {
                        Roles.DeleteRole(t.row.RoleName);
                    }
                }
                JsonResult result = new JsonResult();
                Dictionary<string, bool> response = new Dictionary<string, bool>();
                response.Add("Success", true);
                result.Data = response;
                return result;
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Role/Edit/5

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

        //
        // GET: /Role/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Role/Delete/5

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
