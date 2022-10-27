using System.Security.Authentication;
using AuthorizationServer.Interfaces.Handlers.GrantTypes;
using AuthorizationServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers.GrantTypes;

public class AuthorizationCodeGrantTypeHandler : BaseUserPrincipalHandler,  IAuthorizationCodeGrantTypeHandler
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthorizationCodeGrantTypeHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        : base(signInManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(HttpContext context)
    {
        var result = await context.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            throw new AuthenticationException(result.Failure?.Message);
        }

        var userName = result.Principal!.Identity!.Name;
        var user = await _userManager.FindByNameAsync(userName);
        var principal = await CreateUserPrincipalAsync(context.GetOpenIddictServerRequest(), user);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }
}