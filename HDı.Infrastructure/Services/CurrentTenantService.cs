using HDI.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HDI.Infrastructure.Services;

public class CurrentTenantService(IHttpContextAccessor _httpContextAccessor) : ICurrentTenantService
{
    public int? TenantId { get; set; }

    public int? GetTenantId() => TenantId;

    public string? GetUsername() 
        => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
}