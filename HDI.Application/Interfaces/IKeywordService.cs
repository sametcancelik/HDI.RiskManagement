using HDI.Application.Common;
using HDI.Application.DTOs.Keyword;

namespace HDI.Application.Interfaces;

public interface IKeywordService
{
    Task<ApiResponse<List<KeywordDto>>> GetKeywordsByAgreementIdAsync(int agreementId);
    Task<ApiResponse> AddKeywordAsync(CreateKeywordRequest request);
    Task<ApiResponse> UpdateKeywordWeightAsync(int id, decimal newWeight);
    Task<ApiResponse> DeleteKeywordAsync(int id);
}