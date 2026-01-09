using FluentValidation;
using HDI.Application.DTOs.WorkItem;

public class WorkItemFilterValidator : AbstractValidator<WorkItemFilterRequest>
{
    public WorkItemFilterValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
            .WithMessage("Başlangıç tarihi bitiş tarihinden büyük olamaz.");

        RuleFor(x => x.MinAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum tutar 0'dan küçük olamaz.");
    }
}