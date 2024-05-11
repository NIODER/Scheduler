using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.Groups.Commands.DeleteGroupInvite;

public class DeleteGroupInviteCommandHandler(IGroupsRepository groupsRepository, ILogger<DeleteGroupInviteCommandHandler> logger)
    : IRequestHandler<DeleteGroupInviteCommand, ICommandResult<GroupInviteResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly ILogger<DeleteGroupInviteCommandHandler> _logger = logger;

    public async Task<ICommandResult<GroupInviteResult>> Handle(DeleteGroupInviteCommand request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupInviteResult>($"No group with id {request.GroupId} found.");
        }
        if (!group.UserHasPermissions(new(request.UserId), UserGroupPermissions.DeleteInvites))
        {
            _logger.LogDebug("User {userId} has no permissions to delete group invite.", request.UserId);
            return new AccessViolation<GroupInviteResult>("No permissions.");
        }
        var invite = group.DeleteInvite(new(request.InviteId));
        _groupsRepository.Update(group);
        await _groupsRepository.SaveChangesAsync();
        _logger.LogInformation("Invite {inviteId} deleted from group {groupId}.", invite.Id.Value, group.Id.Value);
        var result = new GroupInviteResult(
            invite.GroupId,
            invite.Id,
            invite.CreatorId,
            invite.Permissions,
            invite.Message);
        return new SuccessResult<GroupInviteResult>(result);
    }
}
