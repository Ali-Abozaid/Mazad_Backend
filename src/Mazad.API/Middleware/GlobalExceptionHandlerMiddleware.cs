using Mazad.SharedKernel.Exceptions;
using System.Net;
using System.Text.Json;

namespace Mazad.API.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            NotFoundException notFound => (HttpStatusCode.NotFound, notFound.Message),
            ValidationException validation => (HttpStatusCode.BadRequest, FormatValidationErrors(validation)),
            ConflictException conflict => (HttpStatusCode.Conflict, conflict.Message),
            UnauthorizedException unauth => (HttpStatusCode.Unauthorized, unauth.Message),
            ForbiddenException forbidden => (HttpStatusCode.Forbidden, forbidden.Message),
            DomainException domain => (HttpStatusCode.BadRequest, domain.Message),
            ArgumentOutOfRangeException argRange => (HttpStatusCode.BadRequest, argRange.Message),
            ArgumentException argument => (HttpStatusCode.BadRequest, argument.Message),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        if (statusCode == HttpStatusCode.InternalServerError)
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        else
            _logger.LogWarning("Domain exception: {Type} — {Message}", exception.GetType().Name, exception.Message);

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = message,
            statusCode = (int)statusCode,
            type = exception.GetType().Name
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }

    private static string FormatValidationErrors(ValidationException ex)
    {
        var errors = ex.Errors.SelectMany(kvp => kvp.Value.Select(v => $"{kvp.Key}: {v}"));
        return string.Join("; ", errors);
    }
}

public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}
