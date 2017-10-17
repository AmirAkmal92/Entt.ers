using System;
using System.Globalization;
using System.Web.Mvc;

namespace Entt.Ers
{
    public class DateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            return value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
        }
    }

}