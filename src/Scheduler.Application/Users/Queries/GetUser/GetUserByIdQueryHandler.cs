using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Queries.GetUser;

public class GetUserByIdQueryHandler(IUsersRepository usersRepository) : IRequestHandler<GetUserByIdQuery, UserResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<UserResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = _usersRepository.GetUserById(new(request.UserId))
            ?? throw new NullReferenceException($"User with id {request.UserId}");
        var userResult = new UserResult(
            user.Id.Value,
            user.Name,
            user.Email,
            user.Description
        );
        return Task.FromResult(userResult);
    }
}
