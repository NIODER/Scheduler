using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate.Events;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Groups.Events;

internal class UserDeletedFromGroupEventHandler(IUsersRepository usersRepository, ILogger<UserDeletedFromGroupEventHandler> logger) 
    : INotificationHandler<UserDeletedFromGroupEvent>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<UserDeletedFromGroupEventHandler> _logger = logger;

    public async Task Handle(UserDeletedFromGroupEvent notification, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetUserByIdAsync(notification.UserId);
        if (user is null)
        {
            _logger.LogInformation("User {userId} was not found.", notification.UserId.Value);
            return;
        }
        user.RemoveGroup(notification.GroupId);
        _usersRepository.Update(user);
        await _usersRepository.SaveChangesAsync();
        _logger.LogInformation("Group {groupId} deleted from user {userId}", notification.GroupId.Value, notification.UserId.Value);
    }
}
