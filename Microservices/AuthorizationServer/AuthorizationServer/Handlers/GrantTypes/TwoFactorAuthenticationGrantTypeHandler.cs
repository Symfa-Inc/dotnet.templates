using AuthorizationServer.Constants;
using AuthorizationServer.Interfaces.Handlers.GrantTypes;
using AuthorizationServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers.GrantTypes;

public class TwoFactorAuthenticationGrantTypeHandler : BaseUserPrincipalHandler, ITwoFactorAuthenticationGrantTypeHandler
{
    private readonly UserManager<ApplicationUser> _userManager;

    public TwoFactorAuthenticationGrantTypeHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        : base(signInManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(HttpContext context)
    {
        var request = context.GetOpenIddictServerRequest();
        if (string.IsNullOrEmpty(request!.Code))
        {
            await ForbidAsync(context, "Code is not provided.");
            return;
        }

        var userId = context.Session.GetString(SessionKeys.TwoFactorAuthentication);
        if (string.IsNullOrEmpty(userId))
        {
            await ForbidAsync(context, "Try to log in first.");
            return;
        }

        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new InvalidOperationException("User is not found.");
        var isValidCode = await _userManager.VerifyTwoFactorTokenAsync(
            user,
            _userManager.Options.Tokens.AuthenticatorTokenProvider,
            request.Code);
        if (!isValidCode)
        {
            await ForbidAsync(context, "Code is not valid.");
            return;
        }

        var principal = await CreateUserPrincipalAsync(request, user);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }

        private static async Task ForbidAsync(HttpContext context, string message)
    {
        var properties = new AuthenticationProperties(
            new Dictionary<string, string>
            {
                { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, message }
            });

        await context.ForbidAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, properties);
    }
}