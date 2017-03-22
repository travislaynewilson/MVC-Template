using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace RxLocal.Web.Framework.Mvc
{
    public class NullJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // http://stackoverflow.com/questions/15939944/jquery-post-json-fails-when-returning-null-from-asp-net-mvc

            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            this.Data = null;

            // If you need special handling, you can call another form of SerializeObject below
            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.None);
            response.Write(serializedObject);
        }
    }
}
