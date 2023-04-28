using CattleMgm.Data.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CattleMgm.Helpers
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, praktikadbContext _context)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, _context);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception,praktikadbContext _context)
        {
            _context.ChangeTracker.Clear();

            Log log = new Log
            {
                UserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier),
                Action = context.Request.RouteValues["action"] == null ? context.Request.RouteValues["page"].ToString() : context.Request.RouteValues["action"].ToString(),
                Url = context.Request.GetDisplayUrl(),
                Ip = context.Connection.RemoteIpAddress.ToString(),
                InsertedDate = DateTime.Now,
                BError = true,
                Exception = JsonConvert.SerializeObject(exception),
                HostName = context.Connection.RemoteIpAddress.ToString(),
                Controller = context.Request.RouteValues["controller"] == null ? context.Request.RouteValues["area"].ToString() : context.Request.RouteValues["controller"].ToString(),
                HttpMethod = context.Request.Method
            };

            if (context.Request.HasFormContentType)
            {
                IFormCollection form = await context.Request.ReadFormAsync();
                log.FormContent = JsonConvert.SerializeObject(form);
            }

            _context.Log.Add(log);
            await _context.SaveChangesAsync();

            if (context.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            else
                context.Response.Redirect("/Home/Error/");

        }
    }
}
