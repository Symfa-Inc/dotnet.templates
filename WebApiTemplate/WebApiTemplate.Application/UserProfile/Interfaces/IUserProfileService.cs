using WebApiTemplate.Application.UserProfile.Models;

namespace WebApiTemplate.Application.UserProfile.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileCreateModelView> CreateUserProfileAsync(UserProfileCreateModel userProfileCreateModel);
        Task<UserProfileGetModelView> GetUserProfileAsync();
        Task<UserProfileUpdateModelView> UpdateUserProfileAsync(UserProfileUpdateModel userProfileUpdateModel);
    }
}
