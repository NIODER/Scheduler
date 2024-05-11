using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Commands.DeleteGroup;

public record DeleteGroupCommand(
    Guid GroupId,
    Guid ExecutorId
) : IRequest<ICommandResult<GroupResult>>;