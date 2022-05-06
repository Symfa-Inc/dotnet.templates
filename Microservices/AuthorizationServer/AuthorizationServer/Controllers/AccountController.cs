using AuthorizationServer.Models.Account;
using AuthorizationServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(model);
        }

        var result = await _userService.CreateUserAsync(model.Username, model.Email, model.Password);
        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest(result.ToString());
    }
}