using HDI.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HDI.WebApi.Controllers;

public class DashboardController(IDashboardService _dashboardService) : BaseController
{
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var response = await _dashboardService.GetTenantDashboardAsync();
        return ActionResultInstance(response);
    }
}