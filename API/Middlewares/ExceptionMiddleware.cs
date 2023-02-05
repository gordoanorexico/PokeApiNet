using Application.Core;
using System.Net;
using System.Text.Json;

namespace API.Middlewares;

/// <summary>
/// Middleware for controlling and logging application errors and then converting those errors to HTTP responses for the API
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    /// <summary>
    /// This method intercepts any call to the service and catches every possible Exception and manage the exception in the desired way
    /// </summary>
    /// <param name="context">the HTTP Context</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            //log the error in the output
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            //set the statuscode as an Internal Server Error
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //in development the extended error will be returned in the response, otherwise a generic Server Error will be returned
            var response = _env.IsDevelopment()
                ? new AppException(context.Response.StatusCode, ex.Message, (ex.StackTrace?.ToString() ?? ""))
                : new AppException(context.Response.StatusCode, "Server error");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}