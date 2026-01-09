using FluentValidation;

public class AnalyzeRiskValidator : AbstractValidator<string> 
{
    public AnalyzeRiskValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Analiz edilecek açıklama metni boş olamaz.")
            .MinimumLength(10).WithMessage("Analiz için en az 10 karakterlik bir metin girilmelidir.");
    }
}