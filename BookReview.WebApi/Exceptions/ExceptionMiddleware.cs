
using System.Net;
using System.Text.Json;

namespace BookReview.WebApi.Exeptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext http)
    {
        try
        {
            await next(http);
        }
        catch (Exception ex)
        {
            logger.LogError($"Something went wrong: {ex.Message}");
            await HandleExceptionAsync(http, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error occurred. Please contact support.",
            Detailed = exception.Message  // Be cautious with sending detailed exception messages in production.
        };

        var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);

        return context.Response.WriteAsync(jsonErrorResponse);
    }
}