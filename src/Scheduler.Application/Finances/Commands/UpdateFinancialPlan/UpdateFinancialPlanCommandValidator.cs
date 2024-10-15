using FluentValidation;

namespace Scheduler.Application.Finances.Commands.UpdateFinancialPlan;

internal class UpdateFinancialPlanCommandValidator : AbstractValidator<UpdateFinancialPlanCommand>
{
    public UpdateFinancialPlanCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FinancialId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(120);
    }
}
