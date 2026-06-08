using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Mazad.SharedKernel.Http;

public class CorrelationIdMiddleware
{
    private const string HeaderName = "X-Correlation-ID";
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers[HeaderName].FirstOrDefault()
            ?? Guid.NewGuid().ToString();

        CorrelationContext.Current = correlationId;
        context.Response.Headers[HeaderName] = correlationId;

        await _next(context);

        CorrelationContext.Current = null;
    }
}

public static class CorrelationIdMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        => app.UseMiddleware<CorrelationIdMiddleware>();
}
