using System;
using System.Web.Mvc;

namespace RxLocal.Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AntiForgeryAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _ignore;

        /// <summary>
        /// Anti-forgery security attribute
        /// </summary>
        /// <param name="ignore">Pass false in order to ignore this security validation</param>
        public AntiForgeryAttribute(bool ignore = false)
        {
            _ignore = ignore;
        }
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            if (_ignore)
                return;

            // Don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;

            // Only POST requests
            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
                return;
            
            var validator = new ValidateAntiForgeryTokenAttribute();
            validator.OnAuthorization(filterContext);
        }
    }
}
