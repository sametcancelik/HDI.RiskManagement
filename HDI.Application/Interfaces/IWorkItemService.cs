using HDI.Application.Common;
using HDI.Application.DTOs.WorkItem;

namespace HDI.Application.Interfaces.Services;

public interface IWorkItemService
{
    Task<ApiResponse<List<WorkItemDto>>> GetWorkItemsByAgreementIdAsync(int agreementId);
    Task<ApiResponse<List<WorkItemDto>>> GetExceededWorkItemsAsync();
    Task<ApiResponse<WorkItemDto?>> GetWorkItemByIdAsync(int id);
}