using AutoMapper;
using HDI.Application.Common;
using HDI.Application.DTOs.Agreement;
using HDI.Application.Exceptions;
using HDI.Application.Interfaces;
using HDI.Application.Interfaces.Persistence;
using HDI.Domain.Entities;

namespace HDI.Application.Services;

public class AgreementService(IUnitOfWork unitOfWork, IMapper mapper) : IAgreementService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponse<List<AgreementDto>>> GetAllAgreementsAsync()
    {
        var agreements = await _unitOfWork.Repository<Agreement, int>().GetAsync();
        var dtos = _mapper.Map<List<AgreementDto>>(agreements);
        
        return ApiResponse<List<AgreementDto>>.Success(dtos);
    }

    public async Task<ApiResponse<AgreementDto?>> GetAgreementByIdAsync(int id)
    {
        var agreement = await _unitOfWork.Repository<Agreement, int>()
            .GetFirstOrDefaultAsync(x => x.Id == id, true, x => x.Keywords);

        if (agreement == null)
            throw new BusinessException("Anlaşma bulunamadı.", 404); // throw kullanımı

        var dto = _mapper.Map<AgreementDto>(agreement);
        return ApiResponse<AgreementDto?>.Success(dto);
    }

    public async Task<ApiResponse> CreateAgreementAsync(CreateAgreementRequest request)
    {
        var hasActiveAgreement = await _unitOfWork.Repository<Agreement, int>()
            .AnyAsync(a => !a.IsDeleted);

        if (hasActiveAgreement)
            throw new BusinessException("Bu iş ortağının zaten aktif bir anlaşması bulunmaktadır."); // Default 400 döner

        var agreement = _mapper.Map<Agreement>(request);

        await _unitOfWork.Repository<Agreement, int>().AddAsync(agreement);
        await _unitOfWork.SaveAsync();

        return ApiResponse.Success("Anlaşma başarıyla oluşturuldu.", 201);
    }

    public async Task<ApiResponse> UpdateAgreementAsync(int id, UpdateAgreementRequest request)
    {
        var existingAgreement = await _unitOfWork.Repository<Agreement, int>().GetByIdAsync(id, false);
        
        if (existingAgreement == null)
            throw new BusinessException("Güncellenecek anlaşma bulunamadı.", 404);

        _mapper.Map(request, existingAgreement);

        _unitOfWork.Repository<Agreement, int>().Update(existingAgreement);
        await _unitOfWork.SaveAsync();

        return ApiResponse.Success("Anlaşma güncellendi.");
    }

    public async Task<ApiResponse> DeleteAgreementAsync(int id)
    {
        var agreement = await _unitOfWork.Repository<Agreement, int>().GetByIdAsync(id, false);
        
        if (agreement == null)
            throw new BusinessException("Silinecek anlaşma bulunamadı.", 404);

        _unitOfWork.Repository<Agreement, int>().Delete(agreement);
        await _unitOfWork.SaveAsync();

        return ApiResponse.Success("Anlaşma başarıyla silindi.");
    }
}