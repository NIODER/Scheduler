using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Queries.GetUser;

public record GetUserByIdQuery(
    Guid UserId
) : IRequest<ICommandResult<UserResult>>;