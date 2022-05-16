using OpenIddict.Abstractions;
using System.Security.Claims;

namespace WebApiTemplate.WebApi.Extensions
{
    public static class UserExtension
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetClaim(OpenIddictConstants.Claims.Subject);
        }
    }
}
