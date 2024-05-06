using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.Common;
using Scheduler.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Application.FriendsInvites.Events;

public class FriendsInviteCreatedEventHandler(IUsersRepository usersRepository, IFriendsInviteRepository friendsInviteRepository)
    : INotificationHandler<FriendsInviteCreatedEvent>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IFriendsInviteRepository _friendsInviteRepository = friendsInviteRepository;

    public async Task Handle(FriendsInviteCreatedEvent notification, CancellationToken cancellationToken)
    {
        User sender = await _usersRepository.GetUserByIdAsync(notification.FriendsInvite.SenderId)
            ?? throw new NullReferenceException($"Invalid sender id {notification.FriendsInvite.SenderId.Value}.");
        User addressie = await _usersRepository.GetUserByIdAsync(notification.FriendsInvite.AddressieId)
            ?? throw new NullReferenceException($"Invalid addressie id {notification.FriendsInvite.AddressieId.Value}.");
        throw new NotImplementedException();
    }
}
