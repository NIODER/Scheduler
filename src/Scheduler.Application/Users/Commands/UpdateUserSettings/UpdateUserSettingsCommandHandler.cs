using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Users.Commands.UpdateUserSettings;

public class UpdateUserSettingsCommandHandler(IUsersRepository usersRepository, ILogger<UpdateUserSettingsCommandHandler> logger) 
    : IRequestHandler<UpdateUserSettingsCommand, ICommandResult<UserSettingsResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<UpdateUserSettingsCommandHandler> _logger = logger;

    public async Task<ICommandResult<UserSettingsResult>> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId != request.ExecutorId)
        {
            return new AccessViolation<UserSettingsResult>("No permissions.");
        }
        User? user = await _usersRepository.GetUserByIdAsync(new UserId(request.UserId));
        if (user is null)
        {
            return new NotFound<UserSettingsResult>($"No user with id {request.UserId} found.");
        }
        user.Settings = (UserPrivateSettings)request.Settings;
        _usersRepository.Update(user);
        await _usersRepository.SaveChangesAsync();
        _logger.LogInformation("User's settings {userId} updated.", user.Id.Value);
        var result = new UserSettingsResult(
            user.Id,
            user.Settings
        );
        return new SuccessResult<UserSettingsResult>(result);
    }
}
