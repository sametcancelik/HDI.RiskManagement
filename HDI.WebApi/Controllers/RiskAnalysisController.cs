using HDI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HDI.WebApi.Controllers;

public class RiskAnalysisController(IRiskAnalysisService _riskAnalysisService) : BaseController
{
    [HttpPost("analyze")]
    public async Task<IActionResult> Analyze(int agreementId, [FromBody] string description)
    {
        var response = await _riskAnalysisService.AnalyzeAndSaveWorkItemAsync(agreementId, description);
        return ActionResultInstance(response);
    }
}