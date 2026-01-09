using FluentValidation;
using HDI.Application.DTOs.Agreement;

namespace HDI.Application.Validators;

public class CreateAgreementValidator : AbstractValidator<CreateAgreementRequest>
{
    public CreateAgreementValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Anlaşma başlığı boş olamaz.")
            .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

        RuleFor(x => x.RiskLimit)
            .GreaterThan(0).WithMessage("Risk limiti 0'dan büyük olmalıdır.");
    }
}