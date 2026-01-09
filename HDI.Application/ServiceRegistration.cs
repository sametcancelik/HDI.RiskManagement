// HDI.Application/ServiceRegistration.cs
using HDI.Application.Interfaces;
using HDI.Application.Interfaces.Services;
using HDI.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HDI.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

        services.AddScoped<IPartnerService, PartnerService>();
        services.AddScoped<IAgreementService, AgreementService>();
        services.AddScoped<IKeywordService, KeywordService>();
        services.AddScoped<IWorkItemService, WorkItemService>();
        services.AddScoped<IRiskAnalysisService, RiskAnalysisService>();
        services.AddScoped<IDashboardService, DashboardService>();}
}