using FluentValidation;

namespace Scheduler.Application.Finances.Queries.GetFinancialPlanById;

internal class GetFInancialPlanByIdQueryValidator : AbstractValidator<GetFinancialPlanByIdQuery>
{
    public GetFInancialPlanByIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEqual(default(Guid));
        RuleFor(x => x.FinancialId).NotEqual(default(Guid));
    }
}
