using MediatR;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Queries.GetUser;

public record GetUserByIdQuery(
    Guid UserId
) : IRequest<UserResult>;