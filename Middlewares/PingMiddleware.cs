using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GuidBot.Middlewares
{
    public class PingMiddleware
    {
        private readonly RequestDelegate _next;

        public PingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Equals("/ping", StringComparison.CurrentCultureIgnoreCase))
            {
                await httpContext.Response.WriteAsync("pong");
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}