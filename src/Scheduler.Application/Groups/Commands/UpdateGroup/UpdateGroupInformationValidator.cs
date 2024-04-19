using FluentValidation;

namespace Scheduler.Application.Groups.Commands.UpdateGroup;

public class UpdateGroupInformationValidator : AbstractValidator<UpdateGroupInformationCommand>
{
    public UpdateGroupInformationValidator()
    {
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.GroupName).MaximumLength(120).NotEmpty();
    }
}