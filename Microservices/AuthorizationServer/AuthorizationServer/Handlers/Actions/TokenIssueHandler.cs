using AuthorizationServer.Constants;
using AuthorizationServer.Interfaces.Handlers.Actions;
using AuthorizationServer.Interfaces.Handlers.GrantTypes;
using Microsoft.AspNetCore;
using OpenIddict.Abstractions;

namespace AuthorizationServer.Handlers.Actions;

public class TokenIssueHandler : ITokenIssueHandler
{
    private readonly IServiceProvider _serviceProvider;

    public TokenIssueHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task IssueAsync(HttpContext context)
    {
        var request = context.GetOpenIddictServerRequest()
            ?? throw new Exception("The OpenID Connect request cannot be retrieved.");
        var handler = GetHandler(request.GrantType);
        await handler.HandleAsync(context);
    }

    private IGrantTypeHandler GetHandler(string requestGrantType)
    {
        return requestGrantType switch
        {
            OpenIddictConstants.GrantTypes.Password => _serviceProvider.GetService<IPasswordGrantTypeHandler>(),
            OpenIddictConstants.GrantTypes.RefreshToken => _serviceProvider.GetService<IRefreshTokenGrantTypeHandler>(),
            OpenIddictConstants.GrantTypes.AuthorizationCode => _serviceProvider.GetService<IAuthorizationCodeGrantTypeHandler>(),
            CustomGrantType.TwoFactorAuthentication => _serviceProvider.GetService<ITwoFactorAuthenticationGrantTypeHandler>(),
            _ => throw new NotImplementedException("The specified grant is not implemented.")
        };
    }
}