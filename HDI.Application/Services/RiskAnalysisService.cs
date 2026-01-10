using HDI.Application.Common;
using HDI.Application.Exceptions;
using HDI.Application.Interfaces;
using HDI.Application.Interfaces.Persistence;
using HDI.Application.Interfaces.Services;
using HDI.Domain.Entities;

namespace HDI.Application.Services;

public class RiskAnalysisService(
    IUnitOfWork unitOfWork, 
    ISignalRService signalRService, 
    ICurrentTenantService currentTenantService) : IRiskAnalysisService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISignalRService _signalRService = signalRService;
    private readonly ICurrentTenantService _currentTenantService = currentTenantService;

    public async Task<ApiResponse<decimal>> AnalyzeAndSaveWorkItemAsync(int agreementId, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new BusinessException("Analiz edilecek açıklama metni boş olamaz.");

        var agreement = await _unitOfWork.Repository<Agreement, int>()
            .GetFirstOrDefaultAsync(x => x.Id == agreementId, true, x => x.Keywords);

        if (agreement == null)
            throw new BusinessException("Geçerli bir anlaşma bulunamadı.", 404);

        decimal totalRiskScore = 0;
        var words = description.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var keyword in agreement.Keywords)
        {
            if (words.Any(w => w.Equals(keyword.Word, StringComparison.OrdinalIgnoreCase)))
            {
                totalRiskScore += keyword.RiskWeight;
            }
        }

        var workItem = new WorkItem
        {
            AgreementId = agreementId,
            Description = description,
            CalculatedRiskAmount = totalRiskScore,
            IsLimitExceeded = totalRiskScore > agreement.RiskLimit,
            TenantId = _currentTenantService.TenantId ?? 0
        };

        await _unitOfWork.Repository<WorkItem, int>().AddAsync(workItem);
        await _unitOfWork.SaveAsync();

        if (workItem.IsLimitExceeded)
        {
            var tenantId = _currentTenantService.TenantId.ToString();
            if  (tenantId == null)
                throw new BusinessException("Tenant bilgisi alınamadı.", 500);

            await _signalRService.SendRiskAlertAsync(tenantId ?? "", new
            {
                Title = "Yüksek Risk Uyarısı!",
                Description = workItem.Description,
                Score = workItem.CalculatedRiskAmount,
                Limit = agreement.RiskLimit
            });
        }

        string statusMessage = workItem.IsLimitExceeded 
            ? "Analiz tamamlandı: Risk limiti aşıldı!" 
            : "Analiz tamamlandı: Risk limit dahilinde.";

        return ApiResponse<decimal>.Success(totalRiskScore, statusMessage);
    }
}