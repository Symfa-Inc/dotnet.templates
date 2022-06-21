using AuthorizationServer.Constants;
using AuthorizationServer.Errors;
using AuthorizationServer.Interfaces.Services;
using AuthorizationServer.Models;
using AuthorizationServer.Models.Account;
using AuthorizationServer.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace AuthorizationServer.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IUserEmailSender _userEmailSender;
    private readonly IUserCreatorService _userCreatorService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(IUserCreatorService userCreatorService, UserManager<ApplicationUser> userManager, IUserEmailSender userEmailSender)
    {
        _userCreatorService = userCreatorService;
        _userManager = userManager;
        _userEmailSender = userEmailSender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        var validationResult = new RegistrationValidator().Validate(model);
        if (!validationResult.IsValid)
        {
            var validationError = validationResult.Errors.First();
            return BadRequest(new ResponseError(validationError.ErrorCode, validationError.ErrorMessage));
        }

        var result = await _userCreatorService.CreateUserAsync(model.Username, model.Email, model.Password);
        if (result.Succeeded)
        {
            return Ok();
        }

        // Note: to see all list of the error codes:
        // typeof(IdentityErrorDescriber).GetProperties().Select(x => x.Name);
        var error = result.Errors.First();
        return BadRequest(new ResponseError(error.Code, error.Description));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        var validationResult = new ForgotPasswordValidator().Validate(model);
        if (!validationResult.IsValid)
        {
            var validationError = validationResult.Errors.First();
            return BadRequest(new ResponseError(validationError.ErrorCode, validationError.ErrorMessage));
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        // For security reasons we won't inform the client whether the user exists
        if (user == null)
        {
            return Ok();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        await _userEmailSender.SendPasswordRecoveryLinkAsync(user.Email, token);
        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        var validationResult = new ResetPasswordModelValidator().Validate(model);
        if (!validationResult.IsValid)
        {
            var validationError = validationResult.Errors.First();
            return BadRequest(new ResponseError(validationError.ErrorCode, validationError.ErrorMessage));
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest(new ResponseError(ErrorCode.UserNotFound));
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (result.Succeeded)
        {
            return Ok();
        }

        var resetPasswordError = result.Errors.First();
        return BadRequest(new ResponseError(resetPasswordError.Code, resetPasswordError.Description));
    }

    [HttpGet("userinfo")]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUserInfo()
    {
        var user = await _userManager.GetUserAsync(User);
        return Ok(
            new
            {
                user.UserName,
                user.Email,
                user.TwoFactorEnabled
            });
    }
}