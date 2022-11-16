using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InteractiveBorderMapApp.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AggregateException exception)
            {
                _logger.Log(LogLevel.Error, DateTime.Now + ": " + exception.StackTrace);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, DateTime.Now + ": " + exception.StackTrace);
            }
        }
    }
}