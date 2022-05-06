using AuthorizationServer.Models;
using AuthorizationServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserAsync(string userName, string email, string password)
    {
        var user = new ApplicationUser { UserName = userName, Email = email };
        return await _userManager.CreateAsync(user, password);
    }
}