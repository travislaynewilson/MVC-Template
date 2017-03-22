using System;
using System.Web.Mvc;
using RxLocal.Core;
using RxLocal.Core.Infrastructure;

namespace RxLocal.Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HttpsRequirementAttribute : FilterAttribute, IAuthorizationFilter
    {
        public HttpsRequirementAttribute(HttpsRequirementType httpsRequirementType)
        {
            HttpsRequirementType = httpsRequirementType;
        }
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            // Don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;
            
            // Only redirect for GET requests, 
            // otherwise the browser might not propagate the verb and request body correctly.
            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;
            
            
            switch (HttpsRequirementType)
            {
                case HttpsRequirementType.Yes:
                    {
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                        var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                        if (!currentConnectionSecured)
                        {
                            // Redirect to HTTPS version of page
                            var url = webHelper.GetThisPageUrl(true, true);

                            //301 (permanent) redirection
                            filterContext.Result = new RedirectResult(url, true);
                        }
                    }
                    break;
                case HttpsRequirementType.No:
                    {
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                        var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                        if (currentConnectionSecured)
                        {
                            // Redirect to HTTP version of page
                            var url = webHelper.GetThisPageUrl(true, false);
                            //301 (permanent) redirection
                            filterContext.Result = new RedirectResult(url, true);
                        }
                    }
                    break;
                case HttpsRequirementType.NoMatter:
                    {
                        //do nothing
                    }
                    break;
                default:
                    throw new RxLocalException("Not supported SslProtected parameter");
            }
        }

        public HttpsRequirementType HttpsRequirementType { get; set; }
    }
}
