using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scheduler.Application.Common.Interfaces.Authentication;
using Scheduler.Application.Common.Interfaces.Services;

namespace Scheduler.Infrastructure.Authentication;

public class JwtTokenGenerator(IOptions<JwtSettings> options, IDateTimeProvider dateTimeProvider) : IJwtTokenGenerator
{
    private readonly JwtSettings jwtSettings = options.Value;

    public string GenerateJwtToken()
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret)
            ),
            SecurityAlgorithms.HmacSha256
        );
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            signingCredentials: credentials,
            expires: dateTimeProvider.UtcNow.AddMinutes(jwtSettings.Expire)
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
