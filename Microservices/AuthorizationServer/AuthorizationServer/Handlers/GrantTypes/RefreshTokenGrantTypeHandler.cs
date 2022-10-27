using System.Security.Authentication;
using AuthorizationServer.Constants;
using AuthorizationServer.Extensions;
using AuthorizationServer.Interfaces.Handlers.GrantTypes;
using AuthorizationServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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
        var result = await context.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            throw new AuthenticationException(result.Failure?.Message);
        }

        // Retrieve the user profile corresponding to the refresh token.
        // Note: if you want to automatically invalidate the refresh token
        // when the user password/roles change, use the following line instead just getting a user:
        // var user = await _signInManager.ValidateSecurityStampAsync(info.Principal);
        var user = await _userManager.GetUserAsync(result.Principal);
        if (user == null)
        {
            await context.BadRequestAsync(ErrorCode.InvalidRefreshToken);
            return;
        }

        var principal = await CreateUserPrincipalAsync(context.GetOpenIddictServerRequest(), user);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }
}