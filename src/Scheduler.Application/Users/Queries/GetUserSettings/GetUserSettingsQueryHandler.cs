using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Users.Queries.GetUserSettings;

public class GetUserSettingsQueryHandler(IUsersRepository usersRepository) 
    : IRequestHandler<GetUserSettingsQuery, ICommandResult<UserSettingsResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<ICommandResult<UserSettingsResult>> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
    {
        if (request.ExecutorId != request.UserId)
        {
            return new AccessViolation<UserSettingsResult>("No permissions.");
        }
        User? user = await _usersRepository.GetUserByIdAsync(new(request.UserId));
        if (user is null)
        {
            return new NotFound<UserSettingsResult>($"User with id {request.UserId} not found.");
        }
        var result = new UserSettingsResult(
            user.Id,
            user.Settings
        );
        return new SuccessResult<UserSettingsResult>(result);
    }
}
