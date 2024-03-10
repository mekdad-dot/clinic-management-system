using ClinicManagementSystem.Application.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace ClinicManagementSystemApi.CustomeMiddlewares.GlobalExceptions
{
    public class GlobalExceptionMiddlerware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddlerware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ServiceException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch(Exception ex)
            {
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                var stackTrace = string.Empty; 
                string message = string.Empty;

                message = ex.Message;
                stackTrace = ex.StackTrace;

                var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;

                await context.Response.WriteAsync(exceptionResult);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ServiceException ex)
        {
            HttpStatusCode statusCode = ex._statusCode;
            var stackTrace = string.Empty; // for another developers in production it should be deleted 
            string message = string.Empty;

            message = ex.Message;
            stackTrace = ex.StackTrace;

            var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(exceptionResult);
        }

    }
}

