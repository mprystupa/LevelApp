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

            if (exception is BusinessValidationException)
            {
                message = exception.Message;
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                StatusCode = context.Response.StatusCode,
                // TODO: Refer to message resources
                Message = message,
                Details = details
            }));
        }
    }
}