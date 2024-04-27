using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Users.Queries.GetUserInvite;

public class GetUserInviteQueryHandler : IRequestHandler<GetUserInviteQuery, UserInviteResult>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserInviteQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<UserInviteResult> Handle(GetUserInviteQuery request, CancellationToken cancellationToken)
    {
        User user = _usersRepository.GetUserById(new(request.UserId))
            ?? throw new NullReferenceException($"No user with id {request.UserId} found.");
        FriendsInvite friendsInvite = user.FriendsInvites.SingleOrDefault(u => u.Id == new FriendsInviteId(request.InviteId))
            ?? throw new NullReferenceException($"No invite with id {request.InviteId} found.");
        var invite = new UserInviteResult(
            friendsInvite.Id,
            friendsInvite.SenderId,
            friendsInvite.AddressieId,
            friendsInvite.Message
        );
        return Task.FromResult(invite);
    }
}
