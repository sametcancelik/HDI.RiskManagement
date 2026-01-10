using Microsoft.AspNetCore.Mvc;
using HDI.Application.DTOs.Account;
using HDI.Application.Interfaces;

namespace HDI.WebApi.Controllers;
public class AuthController(IPartnerService _partnerService) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _partnerService.GetPartnerByApiKeyAsync(request.ApiKey, request.ApiSecret);
        return ActionResultInstance(response);
    }
}
