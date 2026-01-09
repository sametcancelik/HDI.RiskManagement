using FluentValidation;
using HDI.Application.DTOs.Keyword;

namespace HDI.Application.Validators;

public class CreateKeywordValidator : AbstractValidator<CreateKeywordRequest>
{
    public CreateKeywordValidator()
    {
        RuleFor(x => x.Word)
            .NotEmpty().WithMessage("Anahtar kelime boş olamaz.")
            .MinimumLength(2).WithMessage("Kelime en az 2 karakter olmalıdır.");

        RuleFor(x => x.RiskWeight)
            .InclusiveBetween(1, 100).WithMessage("Risk ağırlığı 1 ile 100 arasında olmalıdır.");

        RuleFor(x => x.AgreementId)
            .GreaterThan(0).WithMessage("Geçerli bir anlaşma ID'si gereklidir.");
    }
}