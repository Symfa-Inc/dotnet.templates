using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.UserProfile.Interfaces;
using WebApiTemplate.Application.UserProfile.Models;
using WebApiTemplate.Persistence;
using WebApiTemplate.Domain.Errors.Product;
using Entities = WebApiTemplate.Domain.Entities;

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

        public async Task<UserProfileCreateModelView> CreateUserProfileAsync(UserProfileCreateModel userProfileCreateModel) 
        {
            return null;
        }

        public async Task<UserProfileGetModelView> GetUserProfileAsync()
        {
            return null;
        }

        public async Task<UserProfileUpdateModelView> UpdateUserProfileAsync(UserProfileUpdateModel userProfileUpdateModel)
        {
            return null;
        }
    }
}
