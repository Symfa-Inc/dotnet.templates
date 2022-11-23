using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.UserProfile.Interfaces;
using WebApiTemplate.Application.UserProfile.Models;
using WebApiTemplate.Persistence;
using Entities = WebApiTemplate.Domain.Entities;
using WebApiTemplate.Domain.Errors;
using FluentValidation;
using WebApiTemplate.Application.Extensions;

namespace WebApiTemplate.Application.UserProfile.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly DatabaseContext _context;
        private readonly IValidator<UserProfileCreateModel> _userProfileCreateModelValidator;
        private readonly IValidator<UserProfileUpdateModel> _userProfileUpdateModelValidator;

        public UserProfileService(
            DatabaseContext context,
            IValidator<UserProfileCreateModel> userProfileCreateModelValidator,
            IValidator<UserProfileUpdateModel> userProfileUpdateModelValidator)
        {
            _context = context;
            _userProfileCreateModelValidator = userProfileCreateModelValidator;
            _userProfileUpdateModelValidator = userProfileUpdateModelValidator;
        }

        public async Task<UserProfileCreateModelView> Create(UserProfileInfoModel userProfileInfoModel, UserProfileCreateModel userProfileCreateModel) 
        {
            var validation = _userProfileCreateModelValidator.Validate(userProfileCreateModel);

            if (!validation.IsValid)
            {
                throw new CustomException(validation.ToErrorResponse());
            }

            if (await IsUserProfileExists(userProfileInfoModel))
            {
                throw new CustomException(ErrorCode.EntityAlreadyExists);
            }

            if (userProfileInfoModel.UserId == null || userProfileInfoModel.UserName == null || userProfileInfoModel.Email == null)
            {
                throw new CustomException(ErrorCode.EntityInvalidColumns);
            }

            var userProfile = new Entities.UserProfile
            {
                UserId = userProfileInfoModel.UserId,
                Email = userProfileInfoModel.Email,
                UserName = userProfileInfoModel.UserName,
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
                throw new CustomException(ErrorCode.UserProfileNotFound);
            }

            return userProfile.ToUserProfileGetView();
        }

        public async Task<UserProfileUpdateModelView> Update(string userId, UserProfileUpdateModel userProfileUpdateModel)
        {
            var validation = _userProfileUpdateModelValidator.Validate(userProfileUpdateModel);

            if (!validation.IsValid)
            {
                throw new CustomException(validation.ToErrorResponse());
            }

            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);

            if (userProfile == null)
            {
                throw new CustomException(ErrorCode.UserProfileNotFound);
            }

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

        private async Task<bool> IsUserProfileExists(UserProfileInfoModel userProfileInfoModel)
        {
            return await _context.UserProfiles.AnyAsync(x => x.UserId == userProfileInfoModel.UserId 
                || x.UserName == userProfileInfoModel.UserName
                || x.Email == userProfileInfoModel.Email);
        }
    }
}
