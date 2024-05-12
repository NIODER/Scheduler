using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Commands.UpdateUserSettings;

public record UpdateUserSettingsCommand(
    Guid ExecutorId,
    Guid UserId,
    int Settings
) : IRequest<ICommandResult<UserSettingsResult>>;