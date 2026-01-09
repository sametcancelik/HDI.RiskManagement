using HDI.Application.Common;
using HDI.Application.DTOs.Dashboard;
using HDI.Application.Interfaces;
using HDI.Application.Interfaces.Persistence;
using HDI.Application.Interfaces.Services;
using HDI.Domain.Entities;

namespace HDI.Application.Services;

public class DashboardService(IUnitOfWork unitOfWork, ICurrentTenantService currentTenantService) : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurrentTenantService _currentTenantService = currentTenantService;

    public async Task<ApiResponse<TenantDashboardDto>> GetTenantDashboardAsync()
    {
        var tenantId = _currentTenantService.TenantId;
        
        var allWorkItems = await _unitOfWork.Repository<WorkItem, int>()
            .GetAsync(wi => wi.TenantId == tenantId);

        var totalCount = allWorkItems.Count;
        if (totalCount == 0)
            return ApiResponse<TenantDashboardDto>.Success(new TenantDashboardDto());

        var highRiskItems = allWorkItems.Where(x => x.IsLimitExceeded).ToList();

        var dashboard = new TenantDashboardDto
        {
            TotalAnalysisCount = totalCount,
            HighRiskCount = highRiskItems.Count,
            HighRiskPercentage = Math.Round((double)highRiskItems.Count / totalCount * 100, 2),
            AverageRiskScore = (int)allWorkItems.Average(x => x.CalculatedRiskAmount)
        };

        return ApiResponse<TenantDashboardDto>.Success(dashboard);
    }
}