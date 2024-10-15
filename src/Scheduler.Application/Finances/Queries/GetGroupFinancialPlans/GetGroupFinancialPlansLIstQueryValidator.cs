using FluentValidation;

namespace Scheduler.Application.Finances.Queries.GetGroupFinancialPlans;

internal class GetGroupFinancialPlansLIstQueryValidator : AbstractValidator<GetGroupFinancialPlansListQuery>
{
    public GetGroupFinancialPlansLIstQueryValidator()
    {
        RuleFor(x => x.UserId).NotEqual(default(Guid));
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
    }
}
