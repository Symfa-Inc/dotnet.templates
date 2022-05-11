using AuthorizationServer.Handlers.Interfaces;
using AuthorizationServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers;

public class PasswordGrantTypeHandler : BaseAuthenticateGrantTypeHandler, IPasswordGrantTypeHandler
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
            await ForbidAsync(context);
            return;
        }

        // Validate the username/password parameters and ensure the account is not locked out.
        var result = await SignInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            await ForbidAsync(context);
            return;
        }

        var principal = await CreateUserPrincipalAsync(request, user);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }

    private static async Task ForbidAsync(HttpContext context)
    {
        var properties = new AuthenticationProperties(
            new Dictionary<string, string>
            {
                { OpenIddictServerAspNetCoreConstants.Properties.Error, OpenIddictConstants.Errors.InvalidGrant },
                { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, "Invalid credentials." }
            });
        await context.ForbidAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, properties);
    }
}