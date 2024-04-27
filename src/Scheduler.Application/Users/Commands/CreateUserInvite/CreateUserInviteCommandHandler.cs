using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;

namespace Scheduler.Application.Users.Commands.CreateUserInvite;

public class CreateUserInviteCommandHandler(IUsersRepository usersRepository)
    : IRequestHandler<CreateUserInviteCommand, UserInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<UserInviteResult> Handle(CreateUserInviteCommand request, CancellationToken cancellationToken)
    {
        User sender = _usersRepository.GetUserById(new(request.SenderId))
            ?? throw new NullReferenceException($"No user with id {request.SenderId} not found.");
        User addressie = _usersRepository.GetUserById(new(request.AddressieId))
            ?? throw new NullReferenceException($"No user with id {request.AddressieId} not found.");
        var invite = FriendsInvite.Create(
            senderId: sender.Id,
            addressieId: addressie.Id, 
            request.Message
        );
        addressie.AddFriendsInvite(invite);
        _usersRepository.Update(addressie); // тут я хз, оно sender должно тоже добавиться
        _usersRepository.SaveChanges();
        var result = new UserInviteResult(
            invite.Id,
            invite.SenderId,
            invite.AddressieId,
            invite.Message
        );
        return Task.FromResult(result);
    }
}
