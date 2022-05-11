using AuthorizationServer.Interfaces.Services;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Services;

public class UserCreatorService : IUserCreatorService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserCreatorService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserAsync(string userName, string email, string password)
    {
        var user = new ApplicationUser { UserName = userName, Email = email };
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> CreateUserByExternalInfoAsync(ExternalLoginInfo externalLoginInfo)
    {
        var user = new ApplicationUser { UserName = CreateUserName(externalLoginInfo) };
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            await _userManager.AddLoginAsync(user, externalLoginInfo);
        }

        return result;
    }

    private static string CreateUserName(UserLoginInfo loginInfo)
        => $"{loginInfo.LoginProvider}_{loginInfo.ProviderKey}";
}