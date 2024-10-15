using FluentValidation;

namespace Scheduler.Application.Finances.Commands.CreateFinancialPlan;

internal class CreateFinancialPlanCommandValidator : AbstractValidator<CreateFinancialPlanCommand>
{
    public CreateFinancialPlanCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.Title).NotEmpty();
    }
}
