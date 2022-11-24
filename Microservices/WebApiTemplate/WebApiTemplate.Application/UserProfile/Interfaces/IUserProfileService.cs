using WebApiTemplate.Application.UserProfile.Models;

namespace WebApiTemplate.Application.UserProfile.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileCreateModelView> Create(UserProfileInfoModel userProfileInfoModel, UserProfileCreateModel userProfileCreateModel);
        Task<UserProfileGetModelView> Get(string userId);
        Task<UserProfileUpdateModelView> Update(string userId, UserProfileUpdateModel userProfileUpdateModel);
    }
}
