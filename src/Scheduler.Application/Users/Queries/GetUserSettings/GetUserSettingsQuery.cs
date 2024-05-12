using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Queries.GetUserSettings;

public record GetUserSettingsQuery(
    Guid UserId,
    Guid ExecutorId
) : IRequest<ICommandResult<UserSettingsResult>>;