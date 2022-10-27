using System.Security.Claims;
using AuthorizationServer.Constants;
using AuthorizationServer.Helpers;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;

namespace AuthorizationServer.Handlers;

public abstract class BaseUserPrincipalHandler
{
    protected readonly SignInManager<ApplicationUser> SignInManager;

    protected BaseUserPrincipalHandler(SignInManager<ApplicationUser> signInManager)
    {
        SignInManager = signInManager;
    }

    protected async Task<ClaimsPrincipal> CreateUserPrincipalAsync(OpenIddictRequest request, ApplicationUser user)
    {
        // Create a new ClaimsPrincipal containing the claims that
        // will be used to create an id_token, a token or a code.
        var principal = await SignInManager.CreateUserPrincipalAsync(user);
        principal.SetAudiences(ClientName.BackendClient);

        // Set the list of scopes granted to the client application.
        // Note: the offline_access scope must be granted
        // to allow OpenIddict to return a refresh token.
        principal.SetScopes(GetDefaultAllowedScope().Intersect(request.GetScopes()));
        ClaimsDestinationHelper.SetClaimsDestination(principal);
        return principal;
    }

    private static string[] GetDefaultAllowedScope()
        => new[]
        {
            OpenIddictConstants.Scopes.OfflineAccess,
            OpenIddictConstants.Scopes.Email
        };
}