using System.Security.Claims;
using AuthorizationServer.Constants;
using AuthorizationServer.Helpers;
using AuthorizationServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace AuthorizationServer.Controllers;

public class AuthorizationController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthorizationController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("~/connect/token")]
    [Produces("application/json")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");
        return request.GrantType switch
        {
            OpenIddictConstants.GrantTypes.Password => await HandlePasswordGrantTypeAsync(request),
            OpenIddictConstants.GrantTypes.RefreshToken => await HandleRefreshTokenGrantTypeAsync(),
            _ => throw new NotImplementedException("The specified grant is not implemented.")
        };
    }

    private async Task<IActionResult> HandleRefreshTokenGrantTypeAsync()
    {
        // Retrieve the claims principal stored in the refresh token.
        var info = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

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

            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
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

            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        // Create a new ClaimsPrincipal containing the claims that
        // will be used to create an id_token, a token or a code.
        var principal = await _signInManager.CreateUserPrincipalAsync(user);
        ClaimsDestinationHelper.SetClaimsDestination(principal);
        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    private async Task<IActionResult> HandlePasswordGrantTypeAsync(OpenIddictRequest request)
    {
        IActionResult GetForbidResult()
        {
            var properties = new AuthenticationProperties(
                new Dictionary<string, string>
                {
                    { OpenIddictServerAspNetCoreConstants.Properties.Error, OpenIddictConstants.Errors.InvalidGrant },
                    { OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription, "The username/password couple is invalid." }
                });
            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            return GetForbidResult();
        }

        // Validate the username/password parameters and ensure the account is not locked out.
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return GetForbidResult();
        }

        var principal = await CreateUserPrincipalAsync(request, user);
        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    private static string[] GetDefaultAllowedScope() => new[]
    {
        OpenIddictConstants.Scopes.OpenId,
        OpenIddictConstants.Scopes.OfflineAccess,
        OpenIddictConstants.Scopes.Profile,
        OpenIddictConstants.Scopes.Email
    };

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
}