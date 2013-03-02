using System;
using System.Web.Mvc;
using System.Web.Security;
using CI3540.UI.Filters;
using CI3540.UI.Models;
using CI3540.UI.Services;
using Ninject;
using WebMatrix.WebData;

namespace CI3540.UI.Controllers
{
    [InitializeSimpleMembership]
    [Authorize(Roles = "Admin, Customer")]
    public class AccountController : BootstrapBaseController
    {
        private readonly IUserService userService;

        [Inject]
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult ValidateEmail(string email)
        {
            bool valid = !userService.UserExists(email);
            return Json(valid, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel, string returnUrl, bool navigationLogin = false)
        {
            if (ModelState.IsValid && WebSecurity.Login(viewModel.Email, viewModel.Password, viewModel.RememberMe))
            {
                if (Roles.IsUserInRole("Admin"))
                {
                    var employee = userService.GetEmployeeById(WebSecurity.CurrentUserId);

                    if (employee != null)
                    {
                        Success(string.Format("You have logged in, {0}. Employee Number: [{1}]", employee.Forename, employee.EmployeeNumber));
                    }
                    return RedirectToAction("Index", "Products", new { area = "Admin" });
                }

                if (Roles.IsUserInRole("Customer"))
                {
                    var customer = userService.GetCustomerById(WebSecurity.CurrentUserId);

                    if (customer != null)
                    {
                        Success(string.Format("You have logged in, {0} ", customer.Forename));
                    }

                    return RedirectToAction("Index", "Products", new { area = "Store" });
                }

                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            Information("You have been logged out.");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = userService.CreateCustomer(model);

                    WebSecurity.CreateAccount(customer.Email, model.Password);
                    WebSecurity.Login(customer.Email, model.Password);

                    Attention(string.Format("Welcome {0}, you have logged in.", customer.Forename));
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Manage()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;

                try
                {
                    changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    Success("Your password has been changed.");
                    return RedirectToAction("Manage");
                }

                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }

            return View(model);
        }


        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
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
