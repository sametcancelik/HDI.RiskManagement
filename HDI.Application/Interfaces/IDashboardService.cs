using HDI.Application.Common;
using HDI.Application.DTOs.Dashboard;

namespace HDI.Application.Interfaces.Services;

public interface IDashboardService
{
    Task<ApiResponse<TenantDashboardDto>> GetTenantDashboardAsync();
}