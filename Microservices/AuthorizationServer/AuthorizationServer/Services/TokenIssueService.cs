using AuthorizationServer.Handlers.Interfaces;
using AuthorizationServer.Services.Interfaces;
using Microsoft.AspNetCore;
using OpenIddict.Abstractions;

namespace AuthorizationServer.Services;

public class TokenIssueService : ITokenIssueService
{
    private readonly IServiceProvider _serviceProvider;

    public TokenIssueService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task IssueAsync(HttpContext context)
    {
        var request = context.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");
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
            _ => throw new NotImplementedException("The specified grant is not implemented.")
        };
    }
}