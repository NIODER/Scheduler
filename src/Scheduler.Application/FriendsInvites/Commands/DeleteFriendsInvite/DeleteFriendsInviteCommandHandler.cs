using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.FriendsInvites.Commands.DeleteFriendsInvite;

public class DeleteFriendsInviteCommandHandler(IUsersRepository usersRepository)
        : IRequestHandler<DeleteFriendsInviteCommand, FriendsInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<FriendsInviteResult> Handle(DeleteFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User sender = _usersRepository.GetUserById(new(request.SenderId))
            ?? throw new NullReferenceException($"No user with id {request.SenderId} found.");
        FriendsInvite friendsInvite = sender.DeleteFriendsInvite(new FriendsInviteId(request.InviteId));
        _usersRepository.Update(sender);
        _usersRepository.SaveChanges();
        var result = new FriendsInviteResult(
            friendsInvite.Id,
            friendsInvite.SenderId,
            friendsInvite.AddressieId,
            friendsInvite.Message
        );
        return Task.FromResult(result);
    }
}
