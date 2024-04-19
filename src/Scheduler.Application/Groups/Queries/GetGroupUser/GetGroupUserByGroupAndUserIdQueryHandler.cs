using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.Groups.Queries.GetGroupUser;

public class GetGroupUserByGroupAndUserIdQueryHandler(IGroupsRepository groupsRepository) 
    : IRequestHandler<GetGroupUserByGroupAndUserIdQuery, AccessResultWrapper<GroupUserResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;

    public Task<AccessResultWrapper<GroupUserResult>> Handle(GetGroupUserByGroupAndUserIdQuery request, CancellationToken cancellationToken)
    {
        Group group = _groupsRepository.GetGroupById(new(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} found.");
        var groupUser = group.Users.SingleOrDefault(u => u.UserId.Value == request.UserId);
        if (groupUser is null)
        {
            return Task.FromResult(AccessResultWrapper<GroupUserResult>.CreateForbidden());
        }
        var groupUserResult = new GroupUserResult(
            groupUser.GroupId,
            groupUser.UserId,
            groupUser.Permissions
        );
        return Task.FromResult(AccessResultWrapper<GroupUserResult>.Create(groupUserResult));
    }
}
