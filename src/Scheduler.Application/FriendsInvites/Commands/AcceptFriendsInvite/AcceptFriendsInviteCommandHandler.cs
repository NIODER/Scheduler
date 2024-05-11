using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.FriendsInvites.Commands.AcceptFriendsInvite;

public class AcceptFriendsInviteCommandHandler(IUsersRepository usersRepository)
    : IRequestHandler<AcceptFriendsInviteCommand, ICommandResult<FriendsInviteResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<ICommandResult<FriendsInviteResult>> Handle(AcceptFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetUserByIdAsync(new(request.ExecutorId));
        if (user is null)
        {
            return new NotFound<FriendsInviteResult>($"No user with id {request.ExecutorId} found.");
        }
        FriendsInvite? friendsInvite = user.ReceivedFriendsInvites.SingleOrDefault(fi => fi.Id.Value == request.InviteId);
        if (friendsInvite is null)
        {
            return new NotFound<FriendsInviteResult>($"No friendship invite with id {request.InviteId} found.");
        }
        user.AcceptFriendshipInvite(new FriendsInviteId(request.InviteId));
        _usersRepository.DeleteFriendsInvite(friendsInvite);
        _usersRepository.Update(user);
        await _usersRepository.SaveChangesAsync();
        var result = new FriendsInviteResult(
            friendsInvite.Id,
            friendsInvite.SenderId,
            friendsInvite.AddressieId,
            friendsInvite.Message);
        return new SuccessResult<FriendsInviteResult>(result);
    }
}
