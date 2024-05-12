using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Users.Queries.GetUser;

public class GetUserByIdQueryHandler(IUsersRepository usersRepository) : IRequestHandler<GetUserByIdQuery, ICommandResult<UserResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<ICommandResult<UserResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetUserByIdAsync(new(request.UserId));
        if (user is null)
        {
            return new NotFound<UserResult>($"User with id {request.UserId}");
        }
        var result = new UserResult(
            user.Id.Value,
            user.Name,
            user.Email,
            user.Description
        );
        return new SuccessResult<UserResult>(result);
    }
}
