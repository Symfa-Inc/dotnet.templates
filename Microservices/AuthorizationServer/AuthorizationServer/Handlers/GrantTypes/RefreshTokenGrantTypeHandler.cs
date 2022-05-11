using AuthorizationServer.Helpers;
using AuthorizationServer.Interfaces.Handlers.GrantTypes;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers.GrantTypes;

public class RefreshTokenGrantTypeHandler : IRefreshTokenGrantTypeHandler
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public RefreshTokenGrantTypeHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task HandleAsync(HttpContext context)
    {
        // Retrieve the claims principal stored in the refresh token.
        var info = await context.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        // Retrieve the user profile corresponding to the refresh token.
        // Note: if you want to automatically invalidate the refresh token
        // when the user password/roles change, use the following line instead just getting a user:
        var user = await _signInManager.ValidateSecurityStampAsync(info.Principal);
        if (user == null)
        {
            await ForbidAsync(context, "The refresh token is no longer valid.");
            return;
        }

        // Ensure the user is still allowed to sign in.
        if (!await _signInManager.CanSignInAsync(user))
        {
            await ForbidAsync(context, "The user is no longer allowed to sign in.");
            return;
        }

        // Create a new ClaimsPrincipal containing the claims that
        // will be used to create an id_token, a token or a code.
        var principal = await _signInManager.CreateUserPrincipalAsync(user);
        ClaimsDestinationHelper.SetClaimsDestination(principal);
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