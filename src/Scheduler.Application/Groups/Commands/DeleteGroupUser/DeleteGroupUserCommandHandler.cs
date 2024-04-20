using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Groups.Commands.DeleteGroupUser;

public class DeleteGroupUserCommandHandler(IGroupsRepository groupsRepository) : IRequestHandler<DeleteGroupUserCommand, AccessResultWrapper<GroupUserResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;

    public Task<AccessResultWrapper<GroupUserResult>> Handle(DeleteGroupUserCommand request, CancellationToken cancellationToken)
    {
        Group group = _groupsRepository.GetGroupById(new GroupId(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} found.");
        var user = group.Users.SingleOrDefault(u => u.UserId.Value == request.UserId)
            ?? throw new NullReferenceException($"User with id {request.UserId} not found.");
        if (request.ExecutorId != request.UserId && 
            !group.UserHasPermissions(new UserId(request.ExecutorId), UserGroupPermissions.DeleteGroupUser))
        {
            return Task.FromResult(AccessResultWrapper<GroupUserResult>.CreateForbidden());
        }
        group.DeleteUser(new UserId(request.UserId));
        _groupsRepository.Update(group);
        _groupsRepository.SaveChanges();
        var groupUserResult = new GroupUserResult(
            user.GroupId,
            user.UserId,
            user.Permissions
        );
        return Task.FromResult(AccessResultWrapper<GroupUserResult>.Create(groupUserResult));
    }
}
