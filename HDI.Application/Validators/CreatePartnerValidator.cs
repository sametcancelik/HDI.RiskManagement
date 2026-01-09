using FluentValidation;
using HDI.Application.DTOs.Partner;

public class CreatePartnerValidator : AbstractValidator<CreatePartnerRequest>
{
    public CreatePartnerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Partner adı boş bırakılamaz.")
            .MinimumLength(3).WithMessage("Partner adı en az 3 karakter olmalıdır.");
    }
}