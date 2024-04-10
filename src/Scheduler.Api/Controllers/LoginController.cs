using Microsoft.AspNetCore.Mvc;
using Scheduler.Contracts.Authentication;

namespace Scheduler.Api.Controllers;

[ApiController, Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        throw new NotImplementedException();
    }
}