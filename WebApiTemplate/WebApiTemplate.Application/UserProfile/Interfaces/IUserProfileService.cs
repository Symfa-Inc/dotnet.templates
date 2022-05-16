using WebApiTemplate.Application.UserProfile.Models;

namespace WebApiTemplate.Application.UserProfile.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileCreateModelView> Create(string userId, UserProfileCreateModel userProfileCreateModel);
        Task<UserProfileGetModelView> Get(string userId);
        Task<UserProfileUpdateModelView> Update(string userId, UserProfileUpdateModel userProfileUpdateModel);
    }
}
