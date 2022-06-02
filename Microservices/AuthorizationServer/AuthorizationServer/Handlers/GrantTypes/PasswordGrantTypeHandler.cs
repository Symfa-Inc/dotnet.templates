using AuthorizationServer.Constants;
using AuthorizationServer.Extensions;
using AuthorizationServer.Interfaces.Handlers.GrantTypes;
using AuthorizationServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers.GrantTypes;

public class PasswordGrantTypeHandler : BaseUserPrincipalHandler, IPasswordGrantTypeHandler
{
    private readonly UserManager<ApplicationUser> _userManager;

    public PasswordGrantTypeHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        : base(signInManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(HttpContext context)
    {
        var request = context.GetOpenIddictServerRequest();
        var user = await _userManager.FindByNameAsync(request!.Username);
        if (user == null)
        {
            await context.BadRequestAsync(ErrorCode.InvalidCredentials);
            return;
        }

        // Validate the username/password parameters and ensure the account is not locked out.
        var result = await SignInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            await context.BadRequestAsync(ErrorCode.InvalidCredentials);
            return;
        }

        if (user.TwoFactorEnabled)
        {
            // Set information about the user id that passed credential verification
            context.Session.SetString(SessionKey.TwoFactorAuthentication, user.Id);
            await context.BadRequestAsync(ErrorCode.TwoFactorAuthenticationRequired);
            return;
        }

        var principal = await CreateUserPrincipalAsync(request, user);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }
}