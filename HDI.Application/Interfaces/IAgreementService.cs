using HDI.Application.Common;
using HDI.Application.DTOs.Agreement;

namespace HDI.Application.Interfaces;
public interface IAgreementService
{
    Task<ApiResponse<List<AgreementDto>>> GetAllAgreementsAsync();
    Task<ApiResponse<AgreementDto?>> GetAgreementByIdAsync(int id);
    Task<ApiResponse> CreateAgreementAsync(CreateAgreementRequest request);
    Task<ApiResponse> UpdateAgreementAsync(int id, UpdateAgreementRequest request);
    Task<ApiResponse> DeleteAgreementAsync(int id);
}