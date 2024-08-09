using System.Net;
using System.Text.Json;

namespace API.Middlewares;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await GlobalHandlerException(httpContext, ex);
        }
    }

    private Task ChatHandlerException(HttpContext context, Exception exception)
    {
        _logger.LogError(exception.Message);    
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.NoContent;

        var response = new
        {
            Error = exception.Message,
            Message = "No content"
        };

        var jsonResponse = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }

    private Task AuthHandlerException(HttpContext context, Exception exception)
    {
        _logger.LogError(exception.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var response = new
        {
            Error = exception.Message,
            Message = "Bad request"
        };

        var jsonResponse = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }

    private Task GlobalHandlerException(HttpContext context, Exception exception)
    {
        _logger.LogError(exception.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            Message = "Internal Server error"
        };

        var jsonResponse = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }
}

