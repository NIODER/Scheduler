using MediatR;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Queries;

public record GetGroupByIdQuery(
    Guid GroupId
) : IRequest<GroupResult>;