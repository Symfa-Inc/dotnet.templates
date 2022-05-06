using System.Security.Claims;
using AuthorizationServer.Constants;
using AuthorizationServer.Handlers.Interfaces;
using AuthorizationServer.Helpers;
using AuthorizationServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Handlers;

public class PasswordGrantTypeHandler : IPasswordGrantTypeHandler
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public PasswordGrantTypeHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task HandleAsync(HttpContext context)
    {
        var request = context.GetOpenIddictServerRequest();
        async Task GetForbidResultAsync()
        {
            var properties = new AuthenticationProperties(
                new Dictionary<string, string>
                {
                    { OpenIddictServerAspNetCoreConstants.Properties.Error, OpenIddictConstants.Errors.InvalidGrant },
                    { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, "The username/password couple is invalid." }
                });
            await context.ForbidAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, properties);
        }

        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            await GetForbidResultAsync();
            return;
        }

        // Validate the username/password parameters and ensure the account is not locked out.
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            await GetForbidResultAsync();
            return;
        }

        var principal = await CreateUserPrincipalAsync(request, user);
        await context.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }

    private async Task<ClaimsPrincipal> CreateUserPrincipalAsync(OpenIddictRequest request, ApplicationUser user)
    {
        // Create a new ClaimsPrincipal containing the claims that
        // will be used to create an id_token, a token or a code.
        var principal = await _signInManager.CreateUserPrincipalAsync(user);
        principal.SetAudiences(ClientNames.BackendClient);

        // Set the list of scopes granted to the client application.
        // Note: the offline_access scope must be granted
        // to allow OpenIddict to return a refresh token.
        principal.SetScopes(GetDefaultAllowedScope().Intersect(request.GetScopes()));
        ClaimsDestinationHelper.SetClaimsDestination(principal);
        return principal;
    }

    private static string[] GetDefaultAllowedScope() => new[]
    {
        OpenIddictConstants.Scopes.OpenId,
        OpenIddictConstants.Scopes.OfflineAccess,
        OpenIddictConstants.Scopes.Profile,
        OpenIddictConstants.Scopes.Email
    };
}