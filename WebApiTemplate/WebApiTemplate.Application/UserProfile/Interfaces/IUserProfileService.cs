using WebApiTemplate.Application.UserProfile.Models;

namespace WebApiTemplate.Application.UserProfile.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileCreateModelView> Create(UserProfileCreateModel userProfileCreateModel);
        Task<UserProfileGetModelView> Get();
        Task<UserProfileUpdateModelView> Update(UserProfileUpdateModel userProfileUpdateModel);
    }
}
