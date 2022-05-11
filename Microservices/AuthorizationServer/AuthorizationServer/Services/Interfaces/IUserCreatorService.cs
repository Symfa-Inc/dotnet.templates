using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Services.Interfaces;

public interface IUserCreatorService
{
    Task<IdentityResult> CreateUserAsync(string userName, string email, string password);
    Task<IdentityResult> CreateUserByExternalInfoAsync(ExternalLoginInfo externalLoginInfo);
}