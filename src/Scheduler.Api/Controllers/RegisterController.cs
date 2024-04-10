using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Scheduler.Api.Controllers;

[ApiController, Route("[controller]")]
public class RegisterController : ControllerBase
{
    [HttpPost]
    public IActionResult Registrate([FromBody] RegisterRequest request)
    {
        throw new NotImplementedException();
    }
}