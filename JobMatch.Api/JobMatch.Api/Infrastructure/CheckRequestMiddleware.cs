using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JobMatch.Api.Infrastructure
{
    public class CheckRequestMiddleware
    {
        private RequestDelegate _next;

        public CheckRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!string.IsNullOrWhiteSpace(context.Request.Query["hack"]))
            {
                await context.Response.WriteAsync("are you hacking?");
            }
            else await _next.Invoke(context);

            //calling other middlewares and then adding something to the response.
            //await _next.Invoke(context);
            //if (!string.IsNullOrWhiteSpace(context.Request.Query["hack"]))
            //{
            //    await context.Response.WriteAsync("are you hacking?");
            //}
        }
    }
}
