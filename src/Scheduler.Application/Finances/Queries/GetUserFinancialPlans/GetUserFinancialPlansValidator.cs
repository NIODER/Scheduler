using FluentValidation;

namespace Scheduler.Application.Finances.Queries.GetUserFinancialPlans;

internal class GetUserFinancialPlansValidator : AbstractValidator<GetUserFinancialPlansQuery>
{
    public GetUserFinancialPlansValidator()
    {
        RuleFor(x => x.UserId).NotEqual(default(Guid));
    }
}
