using System;
using System.Net;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LevelApp.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        // TODO: Add logging mechanism

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                // TODO: Log error
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var message = "Something went wrong.";
            var details = exception.Message;
            var responseCode = HttpStatusCode.InternalServerError;

            if (exception is ApiException apiException)
            {
                message = apiException.Message;
                responseCode = apiException.StatusCode;
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseCode;
            
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                context.Response.StatusCode,
                // TODO: Refer to message resources
                Message = message,
                Details = details
            }));
        }
    }
}