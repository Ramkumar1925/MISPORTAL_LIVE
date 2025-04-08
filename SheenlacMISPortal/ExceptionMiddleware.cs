using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SheenlacMISPortal
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               await _next(httpContext);
              //  var response = httpContext.Response;
                //var mathErrorFeature = httpContext.Features.Get<MathErrorFeature>();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                Exceptionlog.Logexception($"Error: {ex.Message}", $"File: {ex.StackTrace}");

                //await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;

            // Customize response for different exceptions
            if (exception is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception is ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Response.StatusCode = statusCode;
           Exceptionlog.Logexception(exception.Message, exception.StackTrace);

            var response = new { message = exception.Message };
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }


        //private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //    var response = new { message = "Internal Server Error from the custom middleware." };
        //    return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        //}
    }
}
