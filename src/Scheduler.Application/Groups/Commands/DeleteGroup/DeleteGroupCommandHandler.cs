using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandHandler(
    IGroupsRepository groupsRepository,
    ILogger<DeleteGroupCommandHandler> logger,
    IUsersRepository usersRepository)
    : IRequestHandler<DeleteGroupCommand, ICommandResult<GroupResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<DeleteGroupCommandHandler> _logger = logger;

    public async Task<ICommandResult<GroupResult>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupResult>($"No group with id {request.GroupId} found.");
        }
        if (!group.UserHasPermissions(new UserId(request.ExecutorId), UserGroupPermissions.IsGroupOwner))
        {
            _logger.LogDebug("User {userId} is not a group owner.", request.ExecutorId);
            return new AccessViolation<GroupResult>("Delete group can only group owner.");
        }
        await DeleteGroupAsync(group.Id);
        await DeleteGroupFromUsers(group.Id);
        return new SuccessResult<GroupResult>(new GroupResult(group));
    }

    private async Task DeleteGroupAsync(GroupId groupId)
    {
        _groupsRepository.DeleteGroupById(groupId);
        await _groupsRepository.SaveChangesAsync();
        _logger.LogInformation("Group {groupId} deleted.", groupId.Value);
    }

    private async Task DeleteGroupFromUsers(GroupId groupId)
    {
        var users = await _usersRepository.GetUsersByGroupIdAsync(groupId);
        foreach (var user in users)
        {
            user.RemoveGroup(groupId);
            _usersRepository.Update(user);
        }
        await _usersRepository.SaveChangesAsync();
        _logger.LogInformation("Group {groupId} deleted from {usersCount} users.", groupId.Value, users.Count);
    }
}
