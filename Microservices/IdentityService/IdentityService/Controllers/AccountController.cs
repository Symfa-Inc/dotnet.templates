using IdentityService.Models;
using IdentityService.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[Route("account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Route("sign-up")]
    [HttpPost]
    public async Task<IActionResult> RegistrationAsync(RegistrationViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newUser = new ApplicationUser(viewModel.UserName);
        var createResult = await _userManager.CreateAsync(newUser, viewModel.Password);
        if (createResult.Succeeded)
        {
            await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, true, false);
            return Ok();
        }

        return BadRequest(createResult.ToString());
    }

    [Route("sign-in")]
    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberLogin, false);
        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest(result.ToString());
    }

    [HttpGet]
    [Route("sign-out")]
    public async Task<IActionResult> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}