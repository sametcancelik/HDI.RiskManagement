using HDI.Application.Common;
using HDI.Application.Exceptions;
using HDI.Application.Interfaces;
using HDI.Application.Interfaces.Persistence;
using HDI.Domain.Entities;

namespace HDI.Application.Services;

public class RiskAnalysisService(IUnitOfWork unitOfWork) : IRiskAnalysisService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
            IsLimitExceeded = totalRiskScore > agreement.RiskLimit
        };

        await _unitOfWork.Repository<WorkItem, int>().AddAsync(workItem);
        await _unitOfWork.SaveAsync();

        string statusMessage = workItem.IsLimitExceeded 
            ? "Analiz tamamlandı: Risk limiti aşıldı!" 
            : "Analiz tamamlandı: Risk limit dahilinde.";

        return ApiResponse<decimal>.Success(totalRiskScore, statusMessage);
    }
}