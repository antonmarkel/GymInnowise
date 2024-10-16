using Newtonsoft.Json;
using System.Net;

namespace GymInnowise.ReportService.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError("An unhandled exception occuried! Details: {@ex}", ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new
            {
                context.Response.StatusCode,
                Message = "Internal Server Error. Please try again later.",
                Details = exception.Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}