using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Groups.Commands.DeleteGroupUser;

public class DeleteGroupUserCommandHandler(IGroupsRepository groupsRepository, ILogger<DeleteGroupUserCommandHandler> logger) 
    : IRequestHandler<DeleteGroupUserCommand, ICommandResult<GroupUserResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly ILogger<DeleteGroupUserCommandHandler> _logger = logger;

    public async Task<ICommandResult<GroupUserResult>> Handle(DeleteGroupUserCommand request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new GroupId(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupUserResult>($"No group with id {request.GroupId} found.");
        }
        GroupUser? user = group.Users.SingleOrDefault(u => u.UserId.Value == request.UserId);
        if (user is null)
        {
            return new NotFound<GroupUserResult>($"User with id {request.UserId} not found in group {group.Id.Value}.");
        }
        if (request.ExecutorId != request.UserId &&
            !group.UserHasPermissions(new UserId(request.ExecutorId), UserGroupPermissions.DeleteGroupUser))
        {
            _logger.LogDebug("User {userId} has no permissions to delete users from group {groupId}", request.ExecutorId, request.GroupId);
            return new AccessViolation<GroupUserResult>("No permissions.");
        }
        group.DeleteUser(new UserId(request.UserId));
        _groupsRepository.Update(group);
        await _groupsRepository.SaveChangesAsync();
        var result = new GroupUserResult(
            user.GroupId,
            user.UserId,
            user.Permissions
        );
        return new SuccessResult<GroupUserResult>(result);
    }
}
