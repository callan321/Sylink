using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebAPI.Application.Contracts.Common; // for OperationResult<Unit>

namespace WebAPI.Api.Middleware
{
    public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception caught by GlobalExceptionMiddleware");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var result = OperationResult<Unit>.Fail(
                    message: "An unexpected error occurred.",
                    errorCode: "UNHANDLED_EXCEPTION"
                );

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
