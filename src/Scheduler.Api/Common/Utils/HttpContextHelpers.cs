using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Scheduler.Api.Common.Utils;

public static class HttpContextHelpers
{
    public static Guid? GetExecutorUserId(this HttpContext context)
    {
        var claimsIdentity = context.User.Identity as ClaimsIdentity;
        var id = claimsIdentity?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (Guid.TryParse(id, out Guid userId))
        {
            return userId;
        }
        return null;
    }
}