using AuthorizationServer.Interfaces.Handlers.GrantTypes;
using AuthorizationServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers.GrantTypes;

public class AuthorizationCodeGrantTypeHandler : BaseAuthenticateGrantTypeHandler,  IAuthorizationCodeGrantTypeHandler
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthorizationCodeGrantTypeHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        : base(signInManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(HttpContext context)
    {
        var request = context.GetOpenIddictServerRequest();
        var result = await context.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            var properties = new AuthenticationProperties(
                new Dictionary<string, string>
                {
                    { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, "Authentication failed." }
                });
            await context.ForbidAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, properties);
            return;
        }

        var userName = result.Principal!.Identity!.Name;
        var user = await _userManager.FindByNameAsync(userName);
        var principal = await CreateUserPrincipalAsync(request, user);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }
}