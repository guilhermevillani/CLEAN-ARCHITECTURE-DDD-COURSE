using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuberDinner.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { error = "Middleware!"+exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);

    }
}