using Application.Models;
using FluentValidation;
using System.Text.Json;

namespace WebAPI.Middlewares
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new Response
            {
                Succeeded = false,
                Title = GetTitle(exception),
                Message = GetErrors(exception),
                SystemError = GetErrors(exception)
            };

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ApplicationException applicationException => applicationException.Message,
                _ => "Server Error"
            };

        private static string GetErrors(Exception exception)
        {
            var errors = string.Empty;

            if (exception is ValidationException validationException)
            {
                errors += String.Join("\r\n", validationException.Errors.Select(e => e.ErrorMessage).ToList());
            }

            return errors;
        }
    }
}
