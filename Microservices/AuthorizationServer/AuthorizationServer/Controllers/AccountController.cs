using AuthorizationServer.Models.Account;
using AuthorizationServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IUserCreatorService _userCreatorService;

    public AccountController(IUserCreatorService userCreatorService)
    {
        _userCreatorService = userCreatorService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(model);
        }

        var result = await _userCreatorService.CreateUserAsync(model.Username, model.Email, model.Password);
        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest(result.ToString());
    }
}