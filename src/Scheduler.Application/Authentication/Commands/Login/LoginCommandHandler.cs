using MediatR;
using Scheduler.Application.Authentication.Common;
using Scheduler.Application.Common.Interfaces.Authentication;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;

namespace Scheduler.Application.Authentication.Commands.Login;

public class LoginCommandHandler(IUsersRepository usersRepository, IHashProvider hashProvider, IJwtTokenGenerator jwtTokenGenerator) 
    : IRequestHandler<LoginCommand, AuthenticationResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IHashProvider _hashProvider = hashProvider;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public Task<AuthenticationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = _usersRepository.GetUserByEmail(request.Email) 
            ?? throw new Exception("No user found with given email.");
        string passwordHash = _hashProvider.GetHash(request.Password);
        if (passwordHash != user.PasswordHash)
        {
            throw new Exception("Invalid password.");
        }
        string token = _jwtTokenGenerator.GenerateJwtToken(user);
        return Task.FromResult(new AuthenticationResult(user, token));
    }
}
