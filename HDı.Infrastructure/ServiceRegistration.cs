using HDI.Application.Interfaces;
using HDI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HDI.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentTenantService, CurrentTenantService>();
        return services;
    }
}