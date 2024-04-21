using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Users.Commands.UpdateUserSettings;

public class UpdateUserSettingsCommandHandler(IUsersRepository usersRepository) : IRequestHandler<UpdateUserSettingsCommand, AccessResultWrapper<UserSettingsResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<AccessResultWrapper<UserSettingsResult>> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId != request.ExecutorId)
        {
            return Task.FromResult(AccessResultWrapper<UserSettingsResult>.CreateForbidden());
        }
        User user = _usersRepository.GetUserById(new UserId(request.UserId))
            ?? throw new NullReferenceException($"No user with id {request.UserId} found.");
        user.SetSettings((UserPrivateSettings)request.Settings);
        _usersRepository.Update(user);
        _usersRepository.SaveChanges();
        var result = new UserSettingsResult(
            user.Id,
            user.Settings
        );
        return Task.FromResult(AccessResultWrapper<UserSettingsResult>.Create(result));
    }
}
