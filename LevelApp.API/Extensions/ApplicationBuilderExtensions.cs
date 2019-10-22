using LevelApp.API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace LevelApp.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}