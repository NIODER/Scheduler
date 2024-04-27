using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Users.Commands.AcceptUserInvite;

public class AcceptUserInviteCommandHandler(IUsersRepository usersRepository) 
    : IRequestHandler<AcceptUserInviteCommand, UserInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<UserInviteResult> Handle(AcceptUserInviteCommand request, CancellationToken cancellationToken)
    {
        User user = _usersRepository.GetUserById(new(request.ExecutorId))
            ?? throw new NullReferenceException($"No user with id {request.ExecutorId} found.");
        var invite = user.AcceptFriendshipInvite(new(request.InviteId));
        return Task.FromResult(
                new UserInviteResult(
                    invite.Id,
                    invite.SenderId,
                    invite.AddressieId,
                    invite.Message
                )
        );
    }
}
