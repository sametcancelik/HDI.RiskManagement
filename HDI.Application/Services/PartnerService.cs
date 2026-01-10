using AutoMapper;
using HDI.Application.Common;
using HDI.Application.DTOs.Partner;
using HDI.Application.Exceptions;
using HDI.Application.Interfaces.Persistence;
using HDI.Application.Interfaces;
using HDI.Domain.Entities;
using HDI.Domain.Common.Helpers;

namespace HDI.Application.Services;

public class PartnerService(IUnitOfWork unitOfWork, IMapper mapper) : IPartnerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponse<PartnerDto>> CreatePartnerAsync(CreatePartnerRequest request)
    {
        var partner = new Partner
        {
            Name = request.Name,
            ApiKey = Guid.NewGuid().ToString("N"),
            IsActive = true
        };

        await _unitOfWork.Repository<Partner, int>().AddAsync(partner);
        await _unitOfWork.SaveAsync();

        var dto = _mapper.Map<PartnerDto>(partner);
        return ApiResponse<PartnerDto>.Success(dto, "Partner başarıyla oluşturuldu.", 201);
    }

    public async Task<ApiResponse<bool>> ValidateApiKeyAsync(string apiKey)
    {
        var isValid = await _unitOfWork.Repository<Partner, int>()
            .AnyAsync(p => p.ApiKey == apiKey && p.IsActive);

        if (!isValid)
            throw new BusinessException("Geçersiz veya pasif API Key!", 401);

        return ApiResponse<bool>.Success(true);
    }

    public async Task<ApiResponse<PartnerDto?>> GetPartnerByApiKeyAsync(string apiKey, string apiSecret)
    {
        var partner = await _unitOfWork.Repository<Partner, int>()
             .GetFirstOrDefaultAsync(p => p.ApiKey == apiKey && p.IsActive);

        if (partner == null || !HashHelper.VerifyHash(apiSecret, partner.ApiSecret))
        {
            throw new BusinessException("Kimlik bilgileri hatalı veya hesap pasif.", 401);
        }

        var dto = _mapper.Map<PartnerDto>(partner);
        return ApiResponse<PartnerDto?>.Success(dto);
    }
    public async Task<ApiResponse<List<PartnerDto>>> GetAllPartnersAsync()
    {
        var partners = await _unitOfWork.Repository<Partner, int>().GetAsync();
        var dtos = _mapper.Map<List<PartnerDto>>(partners);

        return ApiResponse<List<PartnerDto>>.Success(dtos);
    }
}