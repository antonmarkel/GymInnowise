using Newtonsoft.Json;
using System.Net;

namespace GymInnowise.Authorization.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        //TODO: Add logger
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unhandled exception occuried! Details: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //TODO: I guess here we should proceed some of the types of exceptions we can deal with using switch
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