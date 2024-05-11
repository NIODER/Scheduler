using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.Groups.Queries.GetGroupUser;

public class GetGroupUserByGroupAndUserIdQueryHandler(IGroupsRepository groupsRepository, ILogger<GetGroupUserByGroupAndUserIdQueryHandler> logger) 
    : IRequestHandler<GetGroupUserByGroupAndUserIdQuery, ICommandResult<GroupUserResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly ILogger<GetGroupUserByGroupAndUserIdQueryHandler> _logger = logger;

    public async Task<ICommandResult<GroupUserResult>> Handle(GetGroupUserByGroupAndUserIdQuery request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupUserResult>($"No group with id {request.GroupId} found.");
        }
        var groupUser = group.Users.SingleOrDefault(u => u.UserId.Value == request.UserId);
        if (groupUser is null)
        {
            _logger.LogDebug("User {userId} is not member of group {groupId}.", request.UserId, request.GroupId);
            return new AccessViolation<GroupUserResult>("Not a member.");
        }
        var result = new GroupUserResult(
            groupUser.GroupId,
            groupUser.UserId,
            groupUser.Permissions
        );
        return new SuccessResult<GroupUserResult>(result);
    }
}
