using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> CreateUserAsync(string userName, string email, string password);
}