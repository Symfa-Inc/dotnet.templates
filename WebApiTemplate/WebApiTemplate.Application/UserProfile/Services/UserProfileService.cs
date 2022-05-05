using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.UserProfile.Interfaces;
using WebApiTemplate.Application.UserProfile.Models;
using WebApiTemplate.Persistence;
using Entities = WebApiTemplate.Domain.Entities;
using WebApiTemplate.Domain.Errors.Common;

namespace WebApiTemplate.Application.UserProfile.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly DatabaseContext _context;
        private readonly IUserContext _userContext;

        public UserProfileService(DatabaseContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<UserProfileCreateModelView> Create(UserProfileCreateModel userProfileCreateModel) 
        {
            if (_userContext.UserId != userProfileCreateModel.UserId)
            {
                throw new UserIdNotMatchException();
            }

            var userProfile = new Entities.UserProfile
            {
                UserId = _userContext.UserId,
                Email = userProfileCreateModel.Email,
                Name = userProfileCreateModel.Name,
                Surname = userProfileCreateModel.Surname,
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

        public async Task<UserProfileGetModelView> Get()
        {
            var userProfile = await _context.UserProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == _userContext.UserId);

            if (userProfile == null)
            {
                throw new EntityNotFoundException();
            }

            return userProfile.ToUserProfileGetView();
        }

        public async Task<UserProfileUpdateModelView> Update(UserProfileUpdateModel userProfileUpdateModel)
        {
            if (_userContext.UserId != userProfileUpdateModel.UserId)
            {
                throw new UserIdNotMatchException();
            }

            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == _userContext.UserId);

            if (userProfile == null)
            {
                throw new EntityNotFoundException();
            }

            userProfile.Name = userProfileUpdateModel.Name;
            userProfile.Surname = userProfileUpdateModel.Surname;
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
    }
}
