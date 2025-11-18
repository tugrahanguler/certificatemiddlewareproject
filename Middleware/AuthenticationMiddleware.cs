using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    public class SimpleLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public SimpleLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the incoming request
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            // Log the outgoing response
            Console.WriteLine($"Response: {context.Response.StatusCode}");
        }
    }

    public static class SimpleLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseSimpleLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleLoggingMiddleware>();
        }
    }
}