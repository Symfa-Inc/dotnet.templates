using AuthorizationServer.Handlers.Interfaces;
using AuthorizationServer.Helpers;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers;

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
            var properties = new AuthenticationProperties(
                new Dictionary<string, string>
                {
                    { OpenIddictServerAspNetCoreConstants.Properties.Error, OpenIddictConstants.Errors.InvalidGrant },
                    { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, "The refresh token is no longer valid." }
                });

            await context.ForbidAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, properties);
        }

        // Ensure the user is still allowed to sign in.
        if (!await _signInManager.CanSignInAsync(user))
        {
            var properties = new AuthenticationProperties(
                new Dictionary<string, string>
                {
                    { OpenIddictServerAspNetCoreConstants.Properties.Error, OpenIddictConstants.Errors.InvalidGrant },
                    { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, "The user is no longer allowed to sign in." }
                });

            await context.ForbidAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, properties);
        }

        // Create a new ClaimsPrincipal containing the claims that
        // will be used to create an id_token, a token or a code.
        var principal = await _signInManager.CreateUserPrincipalAsync(user);
        ClaimsDestinationHelper.SetClaimsDestination(principal);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }
}