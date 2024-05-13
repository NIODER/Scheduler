using MediatR;
using Scheduler.Application.Authentication.Common;
using Scheduler.Application.Common.Interfaces.Authentication;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Authentication.Commands.Login;

public class LoginCommandHandler(IUsersRepository usersRepository, IHashProvider hashProvider, IJwtTokenGenerator jwtTokenGenerator) 
    : IRequestHandler<LoginCommand, ICommandResult<AuthenticationResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IHashProvider _hashProvider = hashProvider;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ICommandResult<AuthenticationResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetUserByEmailAsync(request.Email);
        if (user is null)
        {
            return new NotFound<AuthenticationResult>("No user found with given email.");
        }
        string passwordHash = _hashProvider.GetHash(request.Password);
        if (passwordHash != user.PasswordHash)
        {
            return new InvalidData<AuthenticationResult>("Invalid password.", nameof(request.Password));
        }
        string token = _jwtTokenGenerator.GenerateJwtToken(user);
        var result = new AuthenticationResult(user, token);
        return new SuccessResult<AuthenticationResult>(result);
    }
}
