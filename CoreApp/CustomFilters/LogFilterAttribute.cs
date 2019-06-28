using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace CoreApp.CustomFilters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        void LogRequest(string state, RouteData route)
        {
            string logMessage = $"Current request state is {state}" +
                $" in controller {route.Values["controller"]} " +
                $"in action {route.Values["action"]}";
            Debug.WriteLine(logMessage);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LogRequest("OnActionExecuting", context.RouteData);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            LogRequest("OnActionExecuted", context.RouteData);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            LogRequest("OnResultExecuting", context.RouteData);
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            LogRequest("OnResultExecuted", context.RouteData);
        }
    }
}
