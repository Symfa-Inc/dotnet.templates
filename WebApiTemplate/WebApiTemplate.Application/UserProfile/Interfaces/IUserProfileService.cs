using WebApiTemplate.Application.UserProfile.Models;

namespace WebApiTemplate.Application.UserProfile.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileCreateModelView> CreateUserProfile(UserProfileCreateModel userProfileCreateModel);
        Task<UserProfileGetModelView> GetUserProfile();
        Task<UserProfileUpdateModelView> UpdateUserProfile(UserProfileUpdateModel userProfileUpdateModel);
    }
}
