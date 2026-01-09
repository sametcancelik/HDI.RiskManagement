using HDI.Application.Common;

namespace HDI.Application.Interfaces;

public interface IRiskAnalysisService
{
    Task<ApiResponse<decimal>> AnalyzeAndSaveWorkItemAsync(int agreementId, string description);
}