using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.UserProfile.Interfaces;
using WebApiTemplate.Application.UserProfile.Models;
using WebApiTemplate.Persistence;
using Entities = WebApiTemplate.Domain.Entities;
using WebApiTemplate.Domain.Errors.Common;
using WebApiTemplate.Domain.Errors.UserProfile;

namespace WebApiTemplate.Application.UserProfile.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly DatabaseContext _context;

        public UserProfileService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<UserProfileCreateModelView> Create(string userId, UserProfileCreateModel userProfileCreateModel) 
        {
            if (await IsUserProfileExists(userId))
            {
                throw new EntityAlreadyExistsException();
            }

            var userProfile = new Entities.UserProfile
            {
                UserId = userId,
                Email = userProfileCreateModel.Email,
                UserName = userProfileCreateModel.UserName,
                DateOfBirth = userProfileCreateModel.DateOfBirth,
                Country = userProfileCreateModel.Country,
                City = userProfileCreateModel.City,
                State = userProfileCreateModel.State,
                District = userProfileCreateModel.District,
                PostalCode = userProfileCreateModel.PostalCode,
                Position = userProfileCreateModel.Position
            };

            _context.UserProfiles.Add(userProfile);

            await _context.SaveChangesAsync();

            return userProfile.ToUserProfileCreateView();
        }

        public async Task<UserProfileGetModelView> Get(string userId)
        {
            var userProfile = await _context.UserProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (userProfile == null)
            {
                throw new UserProfileNotFoundException();
            }

            return userProfile.ToUserProfileGetView();
        }

        public async Task<UserProfileUpdateModelView> Update(string userId, UserProfileUpdateModel userProfileUpdateModel)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);

            if (userProfile == null)
            {
                throw new UserProfileNotFoundException();
            }

            userProfile.UserName = userProfileUpdateModel.UserName;
            userProfile.DateOfBirth = userProfileUpdateModel.DateOfBirth;
            userProfile.Country = userProfileUpdateModel.Country;
            userProfile.City = userProfileUpdateModel.City;
            userProfile.State = userProfileUpdateModel.State;
            userProfile.District = userProfileUpdateModel.District;
            userProfile.PostalCode = userProfileUpdateModel.PostalCode;
            userProfile.Position = userProfileUpdateModel.Position;

            await _context.SaveChangesAsync();

            return userProfile.ToUserProfileUpdateView();
        }

        private async Task<bool> IsUserProfileExists(string userId)
        {
            return await _context.UserProfiles.AnyAsync(x => x.UserId == userId);
        }
    }
}
