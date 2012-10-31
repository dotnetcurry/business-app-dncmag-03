using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TimeSheetGistics.Data;
using TimeSheetGistics.Models;
using Infragistics.Web.Mvc;
using System.Data;

namespace TimeSheetGistics.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private TimeSheetGisticsContext db = new TimeSheetGisticsContext();
        //GET: All Accounts
        public ActionResult Index()
        {
            MembershipUserCollection collection = Membership.GetAllUsers();
            List<CompanyBranchUser> users = db.CompanyBranchUsers.Include(c => c.ParentBranch).ToList<CompanyBranchUser>();
            foreach (MembershipUser item in collection)
            {
                CompanyBranchUser currentUser = null;
                try
                {
                    currentUser = users.Single<CompanyBranchUser>(cbu => cbu.UserName == item.UserName);
                }
                catch(Exception ex)
                {
                    currentUser = new CompanyBranchUser
                    {
                        UserName = item.UserName,
                        Email = item.UserName
                    };
                }
                users.Add(new CompanyBranchUser
                {
                    Email = item.Email,
                    UserName = item.UserName
                });
                
            }
            ViewData["BranchId"] = db.Branches.AsEnumerable<Branch>();
            return View(users.AsQueryable<CompanyBranchUser>());
        }

        public ActionResult Update()
        {
            try
            {
                GridModel m = new GridModel();
                List<Transaction<CompanyBranchUser>> transactions = m.LoadTransactions<CompanyBranchUser>(HttpContext.Request.Form["ig_transactions"]);

                foreach (Transaction<CompanyBranchUser> t in transactions)
                {
                    if (t.type == "newrow")
                    {
                        db.CompanyBranchUsers.Add(t.row);
                        db.SaveChanges();
                    }
                    else if (t.type == "editrow")
                    {
                        db.Entry(t.row).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else if (t.type == "deleterow")
                    {
                        db.Entry(t.row).State = EntityState.Modified;
                        db.SaveChanges();
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
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
