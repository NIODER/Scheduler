using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.GroupInvites.Common;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.GroupInvites.Commands.DeleteGroupInvite;

public class DeleteGroupInviteCommandHandler(IGroupsRepository groupsRepository) : IRequestHandler<DeleteGroupInviteCommand, AccessResultWrapper<GroupInviteResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;

    public async Task<AccessResultWrapper<GroupInviteResult>> Handle(DeleteGroupInviteCommand request, CancellationToken cancellationToken)
    {
        Group group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} found.");
        if (!group.UserHasPermissions(new(request.UserId), UserGroupPermissions.DeleteInvites))
        {
            return AccessResultWrapper<GroupInviteResult>.CreateForbidden();
        }
        var invite = group.DeleteInvite(new(request.InviteId));
        _groupsRepository.Update(group);
        await _groupsRepository.SaveChangesAsync();
        var result = new GroupInviteResult(
            invite.GroupId,
            invite.Id,
            invite.CreatorId,
            invite.Permissions,
            invite.Message);
        return AccessResultWrapper<GroupInviteResult>.Create(result);
    }
}
