using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Queries.GetGroup;

public record GetGroupByIdQuery(
    Guid GroupId
) : IRequest<ICommandResult<GroupResult>>;