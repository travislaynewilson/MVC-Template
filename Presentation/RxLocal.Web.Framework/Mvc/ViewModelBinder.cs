using System.Web.Mvc;

namespace RxLocal.Web.Framework.Mvc
{
    public class ViewModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            if (model is BaseViewModel)
            {
                ((BaseViewModel)model).BindModel(controllerContext, bindingContext);
            }
            return model;
        }
    }
}
