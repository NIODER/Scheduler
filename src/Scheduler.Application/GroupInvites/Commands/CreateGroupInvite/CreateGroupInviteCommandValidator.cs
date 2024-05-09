using FluentValidation;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.GroupInvites.Commands.CreateGroupInvite;

public class CreateGroupInviteCommandValidator : AbstractValidator<CreateGroupInviteCommand>
{
    public CreateGroupInviteCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.CreatorId).NotEqual(default(Guid));
        RuleFor(x => x.Expires).NotNull().When(x => x.Usages is null);
        RuleFor(x => x.Usages).NotNull().When(x => x.Expires is null);
        RuleFor(x => x.Permissions).Must(x => Enum.IsDefined(typeof(UserGroupPermissions), x));
        RuleFor(x => x.Message).MaximumLength(500);
    }
}
