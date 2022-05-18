using AuthorizationServer.Interfaces.Handlers.GrantTypes;
using AuthorizationServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers.GrantTypes;

public class RefreshTokenGrantTypeHandler : BaseUserPrincipalHandler, IRefreshTokenGrantTypeHandler
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RefreshTokenGrantTypeHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        : base(signInManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(HttpContext context)
    {
        // Retrieve the claims principal stored in the refresh token.
        var info = await context.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        // Retrieve the user profile corresponding to the refresh token.
        // Note: if you want to automatically invalidate the refresh token
        // when the user password/roles change, use the following line instead just getting a user:
        // var user = await _signInManager.ValidateSecurityStampAsync(info.Principal);
        var user = await _userManager.GetUserAsync(info.Principal);
        if (user == null)
        {
            await ForbidAsync(context, "The refresh token is no longer valid.");
            return;
        }

        // Ensure the user is still allowed to sign in.
        if (!await SignInManager.CanSignInAsync(user))
        {
            await ForbidAsync(context, "The user is no longer allowed to sign in.");
            return;
        }

        var principal = await CreateUserPrincipalAsync(context.GetOpenIddictServerRequest(), user);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }

    private static async Task ForbidAsync(HttpContext context, string message)
    {
        var properties = new AuthenticationProperties(
            new Dictionary<string, string>
            {
                { OpenIddictServerAspNetCoreConstants.Properties.Error, OpenIddictConstants.Errors.InvalidGrant },
                { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, message }
            });

        await context.ForbidAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, properties);
    }
}