using HDI.Application.DTOs.Partner;
using Microsoft.AspNetCore.Mvc;

namespace HDI.WebApi.Controllers;

public class PartnersController(IPartnerService partnerService) : BaseController
{
    private readonly IPartnerService _partnerService = partnerService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePartnerRequest request)
    {
        var response = await _partnerService.CreatePartnerAsync(request);
        return ActionResultInstance(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _partnerService.GetAllPartnersAsync();
        return ActionResultInstance(response);
    }

    [HttpGet("validate/{apiKey}")]
    public async Task<IActionResult> Validate(string apiKey)
    {
        var response = await _partnerService.GetPartnerByApiKeyAsync(apiKey);
        return ActionResultInstance(response);
    }
}