using HDI.Application.DTOs.WorkItem;
using HDI.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HDI.WebApi.Controllers;

[Route("api/work-items")]
public class WorkItemsController(IWorkItemService workItemService) : BaseController
{
    [HttpGet("agreement/{agreementId}")]
    public async Task<IActionResult> GetByAgreement(int agreementId)
    {
        var response = await workItemService.GetWorkItemsByAgreementIdAsync(agreementId);
        return ActionResultInstance(response);
    }

    [HttpGet("exceeded")]
    public async Task<IActionResult> GetExceeded()
    {
        var response = await workItemService.GetExceededWorkItemsAsync();
        return ActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await workItemService.GetWorkItemByIdAsync(id);
        return ActionResultInstance(response);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> GetFiltered([FromBody] WorkItemFilterRequest filter)
    {
        var response = await workItemService.GetFilteredWorkItemsAsync(filter);
        return ActionResultInstance(response);
    }
}