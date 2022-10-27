using AuthorizationServer.Constants;
using AuthorizationServer.Extensions;
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
            await context.BadRequestAsync(ErrorCode.NoCodeProvided);
            return;
        }

        var userId = context.Session.GetString(SessionKey.TwoFactorAuthentication);
        if (string.IsNullOrEmpty(userId))
        {
            await context.BadRequestAsync(ErrorCode.UnknownUser);
            return;
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            await context.BadRequestAsync(ErrorCode.UserNotFound);
            return;
        }

        var isValidCode = await _userManager.VerifyTwoFactorTokenAsync(
            user,
            _userManager.Options.Tokens.AuthenticatorTokenProvider,
            request.Code);
        if (!isValidCode)
        {
            await context.BadRequestAsync(ErrorCode.InvalidCode);
            return;
        }

        var principal = await CreateUserPrincipalAsync(request, user);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }
}