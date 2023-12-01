using System.Net;

namespace BSES.DocumentManagementSystem
{
    public class HandleExceptionMiddleware
    {
        private readonly ILogger<HandleExceptionMiddleware> _logger;

        private readonly RequestDelegate _next;

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError($"{exception}");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            dynamic result = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error."
            };
            return context.Response.WriteAsync($"{result.Serialize<dynamic>()}");
        }

        public HandleExceptionMiddleware(ILogger<HandleExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
    }
}
    