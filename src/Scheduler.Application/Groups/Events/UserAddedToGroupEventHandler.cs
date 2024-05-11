using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate.Events;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Groups.Events;

internal class UserAddedToGroupEventHandler(IUsersRepository usersRepository, ILogger<UserAddedToGroupEventHandler> logger) 
    : INotificationHandler<UserAddedToGroupEvent>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<UserAddedToGroupEventHandler> _logger = logger;

    public async Task Handle(UserAddedToGroupEvent notification, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetUserByIdAsync(notification.UserId);
        if (user is null)
        {
            _logger.LogInformation("User {userId} wasn't found. Aborting event {eventName}", notification.UserId.Value, nameof(UserAddedToGroupEvent));
            return;
        }
        if (user.IsInGroup(notification.GroupId))
        {
            _logger.LogInformation("User {userId} already in group {groupId}", user.Id.Value, notification.GroupId.Value);
            return;
        }
        user.AddGroup(notification.GroupId);
        _usersRepository.Update(user);
        await _usersRepository.SaveChangesAsync();
        _logger.LogInformation("Group {groupId} added to user {userId} groups list.", notification.GroupId.Value, user.Id.Value);
    }
}
