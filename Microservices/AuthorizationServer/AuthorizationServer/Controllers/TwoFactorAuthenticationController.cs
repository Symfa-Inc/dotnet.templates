using System.Text.Encodings.Web;
using AuthorizationServer.Constants;
using AuthorizationServer.Errors;
using AuthorizationServer.Models;
using AuthorizationServer.Models.TwoFactorAuthentication;
using AuthorizationServer.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace AuthorizationServer.Controllers;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[ApiController]
[Route("two-factor")]
public class TwoFactorAuthenticationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public TwoFactorAuthenticationController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("google")]
    public async Task<IActionResult> GetGoogleAuthenticationInfo()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user.TwoFactorEnabled)
        {
            return BadRequest(new ResponseError(ErrorCode.TwoFactorAlreadyEnabled));
        }

        var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(authenticatorKey))
        {
            // If a user don't have an authenticator key, we have to generate one
            await _userManager.ResetAuthenticatorKeyAsync(user);
            authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        var email = await _userManager.GetEmailAsync(user);
        var authenticationDetailsJson = new
        {
            SecretKey = authenticatorKey,
            AuthenticatorUri = GenerateQrCodeUri(user.UserName, email, authenticatorKey)
        };

        return Ok(authenticationDetailsJson);
    }

    [HttpPost("google-enable")]
    public async Task<IActionResult> EnableGoogleAuthentication(GoogleAuthenticationInfoModel model)
    {
        var validationResult = new GoogleAuthenticationInfoValidator().Validate(model);
        if (!validationResult.IsValid)
        {
            var validationError = validationResult.Errors.First();
            return BadRequest(new ResponseError(validationError.ErrorCode, validationError.ErrorMessage));
        }

        var user = await _userManager.GetUserAsync(User);
        var isValidCode = await _userManager.VerifyTwoFactorTokenAsync(
            user,
            _userManager.Options.Tokens.AuthenticatorTokenProvider,
            model.Code);
        if (isValidCode)
        {
            await _userManager.SetTwoFactorEnabledAsync(user, true);
            return Ok();
        }

        return BadRequest(new ResponseError(ErrorCode.InvalidCode));
    }

    [HttpGet("google-disable")]
    public async Task<IActionResult> DisableGoogleAuthentication()
    {
        var user = await _userManager.GetUserAsync(User);
        if (!user.TwoFactorEnabled)
        {
            return BadRequest(new ResponseError(ErrorCode.TwoFactorAlreadyDisabled));
        }

        await _userManager.SetTwoFactorEnabledAsync(user, false);
        await _userManager.ResetAuthenticatorKeyAsync(user);
        return Ok();
    }

    private static string GenerateQrCodeUri(string accountName, string email, string key)
    {
        const string authenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        var urlEncoder = UrlEncoder.Default;
        return string.Format(
            authenticatorUriFormat,
            urlEncoder.Encode(accountName),
            urlEncoder.Encode(email),
            key);
    }
}