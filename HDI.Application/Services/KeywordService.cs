using AutoMapper;
using HDI.Application.Common;
using HDI.Application.DTOs.Keyword;
using HDI.Application.Exceptions;
using HDI.Application.Interfaces;
using HDI.Application.Interfaces.Persistence;
using HDI.Domain.Entities;

namespace HDI.Application.Services;

public class KeywordService(IUnitOfWork unitOfWork, IMapper mapper) : IKeywordService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponse<List<KeywordDto>>> GetKeywordsByAgreementIdAsync(int agreementId)
    {
        var keywords = await _unitOfWork.Repository<Keyword, int>()
            .GetAsync(x => x.AgreementId == agreementId);
        
        var dtos = _mapper.Map<List<KeywordDto>>(keywords);
        return ApiResponse<List<KeywordDto>>.Success(dtos);
    }

    public async Task<ApiResponse> AddKeywordAsync(CreateKeywordRequest request)
    {
        var exists = await _unitOfWork.Repository<Keyword, int>()
            .AnyAsync(x => x.AgreementId == request.AgreementId && x.Word.ToLower() == request.Word.ToLower());

        if (exists)
            throw new BusinessException("Bu kelime bu anlaşma için zaten tanımlanmış."); // Default 400

        var keyword = _mapper.Map<Keyword>(request);

        await _unitOfWork.Repository<Keyword, int>().AddAsync(keyword);
        await _unitOfWork.SaveAsync();

        return ApiResponse.Success("Kelime başarıyla eklendi.", 201);
    }

    public async Task<ApiResponse> UpdateKeywordWeightAsync(int id, decimal newWeight)
    {
        var keyword = await _unitOfWork.Repository<Keyword, int>().GetByIdAsync(id, false);
        
        if (keyword == null) 
            throw new BusinessException("Kelime bulunamadı.", 404);

        keyword.RiskWeight = newWeight;
        
        _unitOfWork.Repository<Keyword, int>().Update(keyword);
        await _unitOfWork.SaveAsync();

        return ApiResponse.Success("Kelime ağırlığı güncellendi.");
    }

    public async Task<ApiResponse> DeleteKeywordAsync(int id)
    {
        var keyword = await _unitOfWork.Repository<Keyword, int>().GetByIdAsync(id, false);
        
        if (keyword == null)
            throw new BusinessException("Silinecek kelime bulunamadı.", 404);

        _unitOfWork.Repository<Keyword, int>().Delete(keyword);
        await _unitOfWork.SaveAsync();

        return ApiResponse.Success("Kelime başarıyla silindi.");
    }
}