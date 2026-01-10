using HDI.Application.Common;
using HDI.Application.DTOs.Partner;
namespace HDI.Application.Interfaces;
public interface IPartnerService
{
    Task<ApiResponse<PartnerDto>> CreatePartnerAsync(CreatePartnerRequest request);
    Task<ApiResponse<List<PartnerDto>>> GetAllPartnersAsync();
    Task<ApiResponse<bool>> ValidateApiKeyAsync(string apiKey);
    Task<ApiResponse<PartnerDto?>> GetPartnerByApiKeyAsync(string apiKey, string apiSecret);
}