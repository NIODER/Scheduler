using MediatR;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Commands.CreateGroup;

public record CreateGroupCommand(
    Guid ExecutorId,
    string GroupName
) : IRequest<GroupResult>;