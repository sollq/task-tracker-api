using System.Net;
using Newtonsoft.Json;
using TaskTrackerAPI.Controllers;

namespace TaskTrackerAPI.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogInformation("Changing task to comleted state {Id}", ex.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new { message = $"An unexpected error occurred:{ex.Message}" };
        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}