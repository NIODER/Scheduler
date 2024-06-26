using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;

namespace Scheduler.Application.FriendsInvites.Queries.GetFriendsInvite;

public class GetFriendsInviteQueryHandler(IUsersRepository usersRepository) 
    : IRequestHandler<GetFriendsInviteQuery, ICommandResult<FriendsInviteResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<ICommandResult<FriendsInviteResult>> Handle(GetFriendsInviteQuery request, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetUserByIdAsync(new(request.UserId));
        if (user is null)
        {
            return new NotFound<FriendsInviteResult>($"No user with id {request.UserId} found.");
        }
        FriendsInvite? friendsInvite = user.FriendsInvites.SingleOrDefault(fi => fi.Id.Value == request.InviteId);
        if (friendsInvite is null)
        {
            return new NotFound<FriendsInviteResult>($"No friends invite with id {request.InviteId} found.");
        }
        if (!friendsInvite.UserIsRelated(user.Id))
        {
            throw new Exception($"User isn't related to invite.");
        }
        var invite = new FriendsInviteResult(
            friendsInvite.Id,
            friendsInvite.SenderId,
            friendsInvite.AddressieId,
            friendsInvite.Message
        );
        return new SuccessResult<FriendsInviteResult>(invite);
    }
}
