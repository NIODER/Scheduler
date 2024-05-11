using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Queries.GetGroupUser;

public record GetGroupUserByGroupAndUserIdQuery(
    Guid GroupId,
    Guid UserId
) : IRequest<ICommandResult<GroupUserResult>>;