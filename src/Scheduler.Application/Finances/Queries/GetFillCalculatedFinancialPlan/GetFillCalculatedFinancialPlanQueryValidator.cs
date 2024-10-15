using FluentValidation;

namespace Scheduler.Application.Finances.Queries.GetFillCalculatedFinancialPlan;

internal class GetFillCalculatedFinancialPlanQueryValidator : AbstractValidator<GetFillCalculatedFinancialPlanQuery>
{
    public GetFillCalculatedFinancialPlanQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FinancialPlanId).NotEmpty();
        RuleFor(x => x.Budget).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Priority).NotEmpty();
    }
}
