using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.FriendsInvites.Commands.AcceptFriendsInvite;

public class AcceptFriendsInviteCommandHandler(IUsersRepository usersRepository)
    : IRequestHandler<AcceptFriendsInviteCommand, FriendsInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<FriendsInviteResult> Handle(AcceptFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User user = _usersRepository.GetUserById(new(request.ExecutorId))
            ?? throw new NullReferenceException($"No user with id {request.ExecutorId} found.");
        FriendsInvite friendsInvite = user.ReceivedFriendsInvites.SingleOrDefault(fi => fi.Id.Value == request.InviteId)
            ?? throw new NullReferenceException($"No friendship invite with id {request.InviteId} found.");
        user.AcceptFriendshipInvite(new FriendsInviteId(request.InviteId));
        return Task.FromResult(
                new FriendsInviteResult(
                    friendsInvite.Id,
                    friendsInvite.SenderId,
                    friendsInvite.AddressieId,
                    friendsInvite.Message
                )
        );
    }
}
