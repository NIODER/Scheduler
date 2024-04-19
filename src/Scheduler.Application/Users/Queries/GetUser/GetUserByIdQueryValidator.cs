using FluentValidation;

namespace Scheduler.Application.Users.Queries.GetUser;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEqual(default(Guid));
    }
}