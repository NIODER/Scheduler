using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Groups.Commands.CreateGroupInvite;

public class CreateGroupInviteCommandHandler(
    IGroupsRepository groupsRepository,
    IUsersRepository usersRepository,
    ILogger<CreateGroupInviteCommandHandler> logger,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateGroupInviteCommand, ICommandResult<GroupInviteResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<CreateGroupInviteCommandHandler> _logger = logger;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<ICommandResult<GroupInviteResult>> Handle(CreateGroupInviteCommand request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupInviteResult>($"No group with id {request.GroupId} found.");
        }

        User? user = await _usersRepository.GetUserByIdAsync(new(request.CreatorId));
        if (user is null)
        {
            return new NotFound<GroupInviteResult>($"No user with id {request.CreatorId} found.");
        }

        GroupUser? groupUser = group.Users.SingleOrDefault(u => u.UserId == user.Id);
        if (groupUser == default)
        {
            _logger.LogDebug("User with id {userId} is not a member of group with id {groupId}.", user.Id.Value, group.Id.Value);
            return new AccessViolation<GroupInviteResult>("Not member of group.");
        }

        if (!groupUser.Permissions.HasFlag(UserGroupPermissions.CreateInvite))
        {
            _logger.LogDebug(
                "User with id {userId} has no permissions to create group invite for group with id {groupId}.",
                user.Id.Value, group.Id.Value);
            return new AccessViolation<GroupInviteResult>("No permissions.");
        }

        var invite = group.CreateInvite(
            user.Id,
            (UserGroupPermissions)request.Permissions,
            request.Message,
            _dateTimeProvider.UtcNow,
            request.Usages);

        _groupsRepository.Update(group);
        await _groupsRepository.SaveChangesAsync();

        _logger.LogInformation("Group invite {inviteId} created for group {groupId}.", invite.Id.Value, group.Id.Value);

        var result = new GroupInviteResult(
            group.Id,
            invite.Id,
            user.Id,
            invite.Permissions,
            invite.Message
        );

        return new SuccessResult<GroupInviteResult>(result);
    }
}
