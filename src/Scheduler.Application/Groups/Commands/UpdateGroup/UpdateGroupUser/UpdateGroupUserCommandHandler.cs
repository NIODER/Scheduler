using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Commands.UpdateGroupUser;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Groups.Commands.UpdateGroup.UpdateGroupUser;

public class UpdateGroupUserCommandHandler(IGroupsRepository groupsRepository, ILogger<UpdateGroupUserCommandHandler> logger)
    : IRequestHandler<UpdateGroupUserCommand, ICommandResult<GroupUserResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly ILogger<UpdateGroupUserCommandHandler> _logger = logger;

    public async Task<ICommandResult<GroupUserResult>> Handle(UpdateGroupUserCommand request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new GroupId(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupUserResult>($"No group with id {request.GroupId} found.");
        }

        if (!group.UserHasPermissions(new UserId(request.ExecutorId), UserGroupPermissions.ChangeGroupUserSettings))
        {
            _logger.LogDebug("User {userId} has no permissions to change group user settings", request.UserId);
            return new AccessViolation<GroupUserResult>("No permissions.");
        }

        var groupUser = group.Users.SingleOrDefault(x => x.UserId.Value == request.UserId);
        if (groupUser == default)
        {
            return new NotFound<GroupUserResult>($"Group user {request.UserId} was not found.");
        }

        var updatedGroupUser = new GroupUser(
            groupUser.UserId,
            groupUser.GroupId,
            (UserGroupPermissions)request.Permissions
        );

        group.UpdateUser(updatedGroupUser);

        _groupsRepository.Update(group);
        await _groupsRepository.SaveChangesAsync();

        _logger.LogInformation("User {userId} in group {groupId} updated.", request.UserId, request.GroupId);

        var result = new GroupUserResult(
            updatedGroupUser.GroupId,
            updatedGroupUser.UserId,
            updatedGroupUser.Permissions
        );

        return new SuccessResult<GroupUserResult>(result);
    }
}
