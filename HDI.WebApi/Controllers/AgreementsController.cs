using HDI.Application.DTOs.Agreement;
using HDI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HDI.WebApi.Controllers;

public class AgreementsController(IAgreementService agreementService) : BaseController
{
    private readonly IAgreementService _agreementService = agreementService;

    [HttpGet]
    public async Task<IActionResult> GetAll() 
    {
        var response = await _agreementService.GetAllAgreementsAsync();
        return ActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) 
    {
        var response = await _agreementService.GetAgreementByIdAsync(id);
        return ActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAgreementRequest request) 
    {
        var response = await _agreementService.CreateAgreementAsync(request);
        return ActionResultInstance(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAgreementRequest request) 
    {
        var response = await _agreementService.UpdateAgreementAsync(id, request);
        return ActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) 
    {
        var response = await _agreementService.DeleteAgreementAsync(id);
        return ActionResultInstance(response);
    }
}