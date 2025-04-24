using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using PCI.Shared.Common;

namespace PCI.WebAPI.Middleware;

public class ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger = logger;

    public void ConfigureExceptionHandler(IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var ex = contextFeature?.Error;

                if (ex != null)
                {
                    var problem = new ApiResponse<object>
                    {
                        Success = false,
                        Errors = [new ApiError("ErrorOccurred", $"An error occurred: {ex}")],
                    };

                    ErrorLogGenerate(context, ex);
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(problem));
                }
            });
        });
    }

    private void ErrorLogGenerate(HttpContext context, Exception ex)
    {
        string IpAddress = context.Connection.LocalIpAddress.ToString();
        string userId = context.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        string email = context.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        string path = context.Request.Path;
        string StackTrace = ex?.StackTrace;

        string message = $"IpAddress - {context.Connection.LocalIpAddress}, " +
            $"UserId - {userId}, UserEmail - {context.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value}, " +
            $"RequestPath - {context.Request.Path}, Response - {ex?.StackTrace} at {DateTime.Now}";

        _logger.LogError(message);
    }
}
