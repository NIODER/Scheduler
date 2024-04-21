using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Users.Queries.GetUserSettings;

public class GetUserSettingsQueryHandler(IUsersRepository usersRepository) : IRequestHandler<GetUserSettingsQuery, AccessResultWrapper<UserSettingsResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<AccessResultWrapper<UserSettingsResult>> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
    {
        if (request.ExecutorId != request.UserId)
        {
            return Task.FromResult(AccessResultWrapper<UserSettingsResult>.CreateForbidden());
        }
        User user = _usersRepository.GetUserById(new(request.UserId))
            ?? throw new NullReferenceException($"User with id {request.UserId} not found.");
        var userSettingsResult = new UserSettingsResult(
            user.Id,
            user.Settings
        );
        return Task.FromResult(AccessResultWrapper<UserSettingsResult>.Create(userSettingsResult));
    }
}
