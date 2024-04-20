using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Groups.Commands.UpdateGroupUser;

public class UpdateGroupUserCommandHandler(IGroupsRepository groupsRepository) : IRequestHandler<UpdateGroupUserCommand, AccessResultWrapper<GroupUserResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;

    public Task<AccessResultWrapper<GroupUserResult>> Handle(UpdateGroupUserCommand request, CancellationToken cancellationToken)
    {
        Group group = _groupsRepository.GetGroupById(new GroupId(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} found.");
        if (!group.UserHasPermissions(new UserId(request.ExecutorId), UserGroupPermissions.ChangeGroupUserSettings))
        {
            return Task.FromResult(AccessResultWrapper<GroupUserResult>.CreateForbidden());
        }
        var groupUser = group.Users.SingleOrDefault(x => x.UserId.Value == request.UserId);
        if (groupUser is null)
        {
            return Task.FromResult(AccessResultWrapper<GroupUserResult>.CreateForbidden());
        }
        var updatedGroupUser = new GroupUser(
            groupUser.UserId,
            groupUser.GroupId,
            (UserGroupPermissions)request.Permissions
        );
        group.UpdateUser(updatedGroupUser);
        _groupsRepository.Update(group);
        _groupsRepository.SaveChanges();
        var groupUserResult = new GroupUserResult(
            updatedGroupUser.GroupId, 
            updatedGroupUser.UserId, 
            updatedGroupUser.Permissions
        );
        return Task.FromResult(AccessResultWrapper<GroupUserResult>.Create(groupUserResult));
    }
}
