using System;
using System.IO;
using System.Web.Mvc;
using System.Web.WebPages;
using RxLocal.Core;
using RxLocal.Core.Infrastructure;

namespace RxLocal.Web.Framework.ViewEngines.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public IWorkContext WorkContext { get; private set; }

        public override void InitHelpers()
        {
            base.InitHelpers();

            WorkContext = EngineContext.Current.Resolve<IWorkContext>();
        }
        
        public HelperResult RenderSection(string sectionName, Func<object, HelperResult> defaultContent)
        {
            return IsSectionDefined(sectionName) ? RenderSection(sectionName) : defaultContent(new object());
        }

        public override string Layout
        {
            get
            {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = Path.GetFileNameWithoutExtension(layout);
                    var viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");

                    var view = viewResult.View as RazorView;
                    if (view != null)
                    {
                        layout = view.ViewPath;
                    }
                }

                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }
    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}