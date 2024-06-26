using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Commands.CreateGroup;

public record CreateGroupCommand(
    Guid ExecutorId,
    string GroupName
) : IRequest<ICommandResult<GroupResult>>;