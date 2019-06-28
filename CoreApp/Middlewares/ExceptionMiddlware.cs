using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Middlewares
{
    /// <summary>
    /// The class that contains properties for Error Information
    /// </summary>
    public class ErrorInformation
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// This class will contains logic for Exception Middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        // Inject RequestDelegate in ctor
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// This method will contains logc for Middleware
        /// In this case the middleware is sued for Exception Handling 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public  async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // if no error then contrinue to next middleware with HttpContext
                await _next(context);
            }
            catch (Exception ex)
            {
                // Hanlde error and generate response
                HandleError(context, ex);       
            }
        }

        private async void HandleError(HttpContext context, Exception ex)
        {
            // 1. Set the Error Statuc for the Response
            context.Response.StatusCode = 500;
            // 2. Read the Error Message
            string errorMessage = ex.Message;

            // 3. Structurize the ErrorInformation

            var errorInformation = new ErrorInformation()
            {
                 ErrorCode = context.Response.StatusCode,
                 ErrorMessage = errorMessage
            };

            // 4. Write Response in JSON Format
            var errorResponseInformation = JsonConvert.SerializeObject(errorInformation);
            // 4a Write the response
            await context.Response.WriteAsync(errorResponseInformation);
        }
    }

    /// <summary>
    /// The class that will contains an extension method for 
    /// IApplicationBuilder to load and execute ExceptionMiddleware class
    /// </summary>
    public static class CustomExceptionMiddleware
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            // The UserMiddlewate<T>(), check if the T 
            // is injected with RequestDelegate and Contains
            // Invoke() method with HttpContext as parameter
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
