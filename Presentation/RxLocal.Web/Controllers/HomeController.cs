using System.Web.Mvc;
using RxLocal.Core;
using RxLocal.Services.Customers;
using RxLocal.Web.Framework.Controllers;

namespace RxLocal.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;

        public HomeController(
            IWorkContext workContext, 
            ICustomerService customerService)
        {
            _workContext           = workContext;
            _customerService       = customerService;
        }


        public ActionResult Index()
        {
            var customer = _customerService.GetCustomer(_workContext.CurrentCustomer.CustomerID);

            return View(customer);
        }
    }
}