using FluentValidation;

namespace Scheduler.Application.Finances.Commands.DeleteFinancialPlan;

internal class DeleteFinancialPlanCommandValidator : AbstractValidator<DeleteFinancialPlanCommand>
{
    public DeleteFinancialPlanCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FinancialPlanId).NotEmpty();
    }
}
