using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.UserProfile.Interfaces;
using WebApiTemplate.Application.UserProfile.Models;
using WebApiTemplate.Persistence;
using Entities = WebApiTemplate.Domain.Entities;
using WebApiTemplate.Application.Error.Interfaces;
using WebApiTemplate.Domain.Enums.Error;

namespace WebApiTemplate.Application.UserProfile.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly DatabaseContext _context;
        private readonly IUserContext _userContext;
        private readonly IErrorService _errorService;

        public UserProfileService(DatabaseContext context, IUserContext userContext, IErrorService errorService)
        {
            _context = context;
            _userContext = userContext;
            _errorService = errorService;
        }

        public async Task<UserProfileCreateModelView> Create(UserProfileCreateModel userProfileCreateModel) 
        {
            if (userProfileCreateModel == null || _userContext.UserId == null)
            {
                _errorService.Add(ErrorCode.MODEL_IS_INVALID);
                return null;
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
                _errorService.Add(ErrorCode.DATA_NOT_FOUND);
                return null;
            }

            return userProfile.ToUserProfileGetView();
        }

        public async Task<UserProfileUpdateModelView> Update(UserProfileUpdateModel userProfileUpdateModel)
        {
            if (userProfileUpdateModel == null)
            {
                _errorService.Add(ErrorCode.MODEL_IS_INVALID);
                return null;
            }

            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == _userContext.UserId);

            if (userProfile == null)
            {
                _errorService.Add(ErrorCode.DATA_NOT_FOUND);
                return null;
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
