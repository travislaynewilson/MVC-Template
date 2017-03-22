namespace System.Web.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string SignIn(this UrlHelper urlHelper, string returnUrl = "")
        {
            if (!string.IsNullOrEmpty(returnUrl))
                return urlHelper.Action("Login", "Authentication", new { ReturnUrl = returnUrl });
            return urlHelper.Action("Login", "Authentication");
        }

        public static string SignOut(this UrlHelper urlHelper, string returnUrl = "")
        {
            if (!string.IsNullOrEmpty(returnUrl))
                return urlHelper.Action("Logout", "Authentication", new { ReturnUrl = returnUrl });
            return urlHelper.Action("Logout", "Authentication");
        }
    }
}
