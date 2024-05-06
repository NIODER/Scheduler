using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.FriendsInviteAggregate;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.FriendsInvites.Queries.GetFriendsInvite;

public class GetFriendsInviteQueryHandler(IUsersRepository usersRepository, IFriendsInviteRepository friendsInviteRepository) : IRequestHandler<GetFriendsInviteQuery, FriendsInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IFriendsInviteRepository _friendsInviteRepository = friendsInviteRepository;

    public Task<FriendsInviteResult> Handle(GetFriendsInviteQuery request, CancellationToken cancellationToken)
    {
        User user = _usersRepository.GetUserById(new(request.UserId))
            ?? throw new NullReferenceException($"No user with id {request.UserId} found.");
        FriendsInvite friendsInvite = _friendsInviteRepository.GetFriendsInviteById(new(request.InviteId))
            ?? throw new NullReferenceException($"No friends invite with id {request.InviteId} found.");
        if (friendsInvite.UserIsRelated(user.Id))
        {
            throw new Exception($"User isn't related to invite.");
        }
        var invite = new FriendsInviteResult(
            friendsInvite.Id,
            friendsInvite.SenderId,
            friendsInvite.AddressieId,
            friendsInvite.Message
        );
        return Task.FromResult(invite);
    }
}
