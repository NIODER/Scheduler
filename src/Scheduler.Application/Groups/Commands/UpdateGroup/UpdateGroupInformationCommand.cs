using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Commands.UpdateGroup;

public record UpdateGroupInformationCommand(
    Guid GroupId,
    Guid ExecutorId,
    string GroupName
) : IRequest<AccessResultWrapper<GroupResult>>;