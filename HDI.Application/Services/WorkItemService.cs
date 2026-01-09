using AutoMapper;
using HDI.Application.Common;
using HDI.Application.DTOs.WorkItem;
using HDI.Application.Exceptions;
using HDI.Application.Interfaces;
using HDI.Application.Interfaces.Persistence;
using HDI.Application.Interfaces.Services;
using HDI.Domain.Entities;

namespace HDI.Application.Services;

public class WorkItemService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTenantService currentTenantService) : IWorkItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICurrentTenantService _currentTenantService = currentTenantService;

    public async Task<ApiResponse<List<WorkItemDto>>> GetWorkItemsByAgreementIdAsync(int agreementId)
    {
        var workItems = await _unitOfWork.Repository<WorkItem, int>()
            .GetAsync(
                predicate: x => x.AgreementId == agreementId,
                disableTracking: true,
                includes: x => x.Agreement 
            );

        var dtos = _mapper.Map<List<WorkItemDto>>(workItems);
        return ApiResponse<List<WorkItemDto>>.Success(dtos);
    }

    public async Task<ApiResponse<List<WorkItemDto>>> GetExceededWorkItemsAsync()
    {
        var items = await _unitOfWork.Repository<WorkItem, int>()
            .GetAsync(x => x.IsLimitExceeded, true, x => x.Agreement);

        var dtos = _mapper.Map<List<WorkItemDto>>(items);
        return ApiResponse<List<WorkItemDto>>.Success(dtos);
    }

    public async Task<ApiResponse<WorkItemDto?>> GetWorkItemByIdAsync(int id)
    {
        var item = await _unitOfWork.Repository<WorkItem, int>()
            .GetFirstOrDefaultAsync(x => x.Id == id, true, x => x.Agreement);

        if (item == null)
            throw new BusinessException("İş kaydı bulunamadı.", 404);

        var dto = _mapper.Map<WorkItemDto>(item);
        return ApiResponse<WorkItemDto?>.Success(dto);
    }

    public async Task<ApiResponse<List<WorkItemDto>>> GetFilteredWorkItemsAsync(WorkItemFilterRequest filter)
{
    var tenantId = _currentTenantService.TenantId;

    var workItems = await _unitOfWork.Repository<WorkItem, int>().GetAsync(
        predicate: x => x.TenantId == tenantId &&
            (!filter.AgreementId.HasValue || x.AgreementId == filter.AgreementId) &&
            (!filter.StartDate.HasValue || x.CreatedDate >= filter.StartDate) &&
            (!filter.EndDate.HasValue || x.CreatedDate <= filter.EndDate) &&
            (!filter.MinAmount.HasValue || x.CalculatedRiskAmount >= filter.MinAmount) &&
            (!filter.MaxAmount.HasValue || x.CalculatedRiskAmount <= filter.MaxAmount) &&
            (!filter.IsLimitExceeded.HasValue || x.IsLimitExceeded == filter.IsLimitExceeded),
        includes: x => x.Agreement
    );

    return ApiResponse<List<WorkItemDto>>.Success(_mapper.Map<List<WorkItemDto>>(workItems));
}
}