using System.Web;
using RxLocal.Core;
using RxLocal.Core.Caching;
using RxLocal.Core.Domain.Customers;
using RxLocal.Core.Fakes;
using RxLocal.Services;

namespace RxLocal.Web.Framework
{
    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        private const string CurrentCustomerKey = "CURRENTCUSTOMER";

        private readonly HttpContextBase _httpContext;
        private readonly IWebHelper _webHelper;
        private readonly IServiceManager _services;
        private readonly ICacheManager _cacheManager;

        public WebWorkContext(
            HttpContextBase httpContext,
            IWebHelper webHelper,
            IServiceManager services,
            ICacheManager cacheManager)
        {
            _httpContext  = httpContext;
            _webHelper    = webHelper;
            _services     = services;
            _cacheManager = cacheManager;
        }

        

        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        public virtual Customer CurrentCustomer
        {
            get
            {
                return _cacheManager.Get(CurrentCustomerKey, () =>
                {
                    Customer customer = null;
                    if (_httpContext == null || _httpContext is FakeHttpContext)
                    {
                        //check whether request is made by a background task
                        //in this case return built-in customer record for background tasks
                        customer = _services.Customers.GetCustomer(SystemCustomerIDs.MasterAccount);
                    }

                    // Registered user
                    if (customer == null)
                    {
                        customer = _services.Authentication.GetAuthenticatedCustomer();
                    }

                    return customer;
                });
            }
            set
            {
                _cacheManager.Set(CurrentCustomerKey, value, 1);
            }
        }
    }
}