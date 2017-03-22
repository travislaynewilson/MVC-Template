using System.Web.Mvc;
using RxLocal.Core;
using RxLocal.Core.Domain.Customers;
using RxLocal.Services.Authentication;
using RxLocal.Services.Customers;
using RxLocal.Web.Framework.Controllers;
using RxLocal.Web.Framework.Security;
using RxLocal.Web.ViewModels.Authentication;

namespace RxLocal.Web.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            IWorkContext workContext, 
            ICustomerService customerService,
            IAuthenticationService authenticationService)
        {
            _workContext           = workContext;
            _customerService       = customerService;
            _authenticationService = authenticationService;
        }

        public ActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost, AntiForgery]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Customer customer;
            var response = _customerService.AuthenticateCustomer(model.LoginName, model.Password, out customer);

            if (response == CustomerAuthenticationResults.Successful)
            {
                _authenticationService.SignIn(customer, true);
            }
            else
            {
                switch (response)
                {
                    case CustomerAuthenticationResults.CustomerNotExist:
                    case CustomerAuthenticationResults.Deleted:
                        ModelState.AddModelError("CustomerNotExist", "The username you entered does not appear to be valid. Please try again.");
                        break;

                    case CustomerAuthenticationResults.InvalidPassword:
                        ModelState.AddModelError("InvalidPassword", "The password you entered does not appear to be valid. Please try again.");
                        break;

                    case CustomerAuthenticationResults.LoginDisabled:
                        ModelState.AddModelError("LoginDisabled", "Your account has been locked. Please contact customer service for more information.");
                        break;

                    case CustomerAuthenticationResults.NotAuthorized:
                        ModelState.AddModelError("NotAuthorized", string.Format("Welcome back {0}! Unfortunately, we could not grant you access at this time. Please contact customer service for more information."));
                        break;
                }

                return View(model);
            }


            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            _authenticationService.SignOut();

            return RedirectToAction("Login");
        }
    }
}