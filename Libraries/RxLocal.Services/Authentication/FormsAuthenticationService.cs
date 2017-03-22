using System;
using System.Web;
using System.Web.Security;
using RxLocal.Core.Domain.Customers;
using RxLocal.Services.Customers;

namespace RxLocal.Services.Authentication
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly TimeSpan _expirationTimeSpan;

        private Customer _cachedCustomer;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="customerService">Customer service</param>
        /// <param name="customerSettings">Customer settings</param>
        public FormsAuthenticationService(
            HttpContextBase httpContext,
            ICustomerService customerService)
        {
            this._httpContext        = httpContext;
            this._customerService    = customerService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }


        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        public virtual void SignIn(Customer customer, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                2 /*version*/,
                customer.LoginName,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                customer.LoginName,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true
            };

            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedCustomer = customer;
        }

        /// <summary>
        /// Sign out
        /// </summary>
        public virtual void SignOut()
        {
            _cachedCustomer = null;
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        public virtual Customer GetAuthenticatedCustomer()
        {
            if (_cachedCustomer != null)
                return _cachedCustomer;

            if (_httpContext?.Request == null || !_httpContext.Request.IsAuthenticated || !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var customer = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            if (customer != null)
                _cachedCustomer = customer;

            return _cachedCustomer;
        }


        #region Utilities
        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <param name="ticket">Ticket</param>
        /// <returns>Customer</returns>
        protected virtual Customer GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            var loginName = ticket.UserData;

            if (string.IsNullOrWhiteSpace(loginName))
                return null;

            var customer = _customerService.GetCustomer(loginName);

            return customer;
        }
        #endregion
    }
}
