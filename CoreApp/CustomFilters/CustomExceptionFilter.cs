using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CoreApp.CustomFilters
{
    public class CustomExceptionFilter :  IExceptionFilter
    {
        private readonly IModelMetadataProvider metadataProvider;

        // this injection will be managed by MvcOptions 
        public CustomExceptionFilter(IModelMetadataProvider metadataProvider)
        {
            this.metadataProvider = metadataProvider;
        }
        public void OnException(ExceptionContext context)
        {
            // 1. Handle the exception to complete the HttpRequest
            context.ExceptionHandled = true;
            // 2. Read the Exception Message
            string errorMessage = context.Exception.Message;
            // 3. Arrange the result View
            // 3a. Useing ViewResult for the View Name
            var viewResult = new ViewResult()
            {
                 ViewName = "CustomError"
            };

            // 3b. Settings the View Data Dictionary

            viewResult.ViewData = new ViewDataDictionary(metadataProvider, context.ModelState);

            viewResult.ViewData["ControllerName"] = context.RouteData.Values["controller"];
            viewResult.ViewData["ActionName"] = context.RouteData.Values["action"];
            viewResult.ViewData["ErrorMessage"] = errorMessage;

            // 3c. Set value for the Result property

            context.Result = viewResult;
        }
    }
}
