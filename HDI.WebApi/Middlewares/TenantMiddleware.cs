using HDI.Application.Interfaces;

namespace HDI.WebAPI.Middlewares;

public class TenantMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IPartnerService partnerService, ICurrentTenantService currentTenantService)
    {
        if ((context.Request.Path.StartsWithSegments("/api/partners") && context.Request.Method == HttpMethods.Post) || 
        context.Request.Path.StartsWithSegments("/risk-hub") ||
        context.Request.Path.StartsWithSegments("/api/auth/login"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var apiKey) || 
        !context.Request.Headers.TryGetValue("X-Api-Secret", out var apiSecret))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsJsonAsync(new { message = "Eksik Kimlik Bilgileri!" });
        return;
    }

        var partner = await partnerService.GetPartnerByApiKeyAsync(apiKey!, apiSecret!);
        if (partner == null || !partner.Data.IsActive)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Geçersiz veya pasif API Key!");
            return;
        }

        currentTenantService.TenantId = partner.Data.Id;

        await _next(context);
    }
}