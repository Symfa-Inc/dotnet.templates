using System.Security.Authentication;
using AuthorizationServer.Helpers;
using AuthorizationServer.Interfaces.Handlers.Actions;
using AuthorizationServer.Interfaces.Services;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers.Actions;

public class ExternalProviderHandler : IExternalProviderHandler
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserCreatorService _userCreatorService;

    public ExternalProviderHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserCreatorService userCreatorService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userCreatorService = userCreatorService;
    }

    public async Task SignInAsync(HttpContext context, string provider, string redirectUrl)
    {
        var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
        if (externalLoginInfo == null)
        {
            var existingProviders = (await _signInManager.GetExternalAuthenticationSchemesAsync()).Select(x => x.Name);
            if (!existingProviders.Contains(provider, StringComparer.OrdinalIgnoreCase))
            {
                await ForbidAsync(context, "Required provider is not specified.");
                return;
            }

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            await context.ChallengeAsync(provider, properties);
            return;
        }

        var user = await CreateUserIfNotExist(externalLoginInfo);
        var principal = await _signInManager.CreateUserPrincipalAsync(user);
        principal.SetScopes(OpenIddictConstants.Scopes.OfflineAccess);
        ClaimsDestinationHelper.SetClaimsDestination(principal);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }

    private static async Task ForbidAsync(HttpContext context, string message)
    {
        var authenticationProperties = new AuthenticationProperties(
            new Dictionary<string, string>
            {
                { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, message }
            });
        await context.ForbidAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, authenticationProperties);
    }

    private async Task<ApplicationUser> CreateUserIfNotExist(ExternalLoginInfo externalLoginInfo)
    {
        var user = await _userManager.FindByLoginAsync(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey);
        if (user != null)
        {
            return user;
        }

        var result = await _userCreatorService.CreateUserByExternalInfoAsync(externalLoginInfo);
        if (!result.Succeeded)
        {
            throw new AuthenticationException(string.Join("; ", result.Errors));
        }

        return await _userManager.FindByLoginAsync(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey);
    }
}