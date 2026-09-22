using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Homely.Api.Exceptions;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (status, title) = exception switch
        {
            KeyNotFoundException => (404, "Resource not found"),
            InvalidOperationException => (409, "Operation conflict"),
            ArgumentException => (400, "Invalid request"),
            _ => (500, "Internal server error")
        };

        if (status == 500)
        {
            _logger.LogError(
                exception,
                "Unexpected error: {Message}",
                exception.Message);
        }
        else
        {
            _logger.LogWarning(
                "Handled exception [{Status}]: {Message}",
                status,
                exception.Message);
        }

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = status;

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}