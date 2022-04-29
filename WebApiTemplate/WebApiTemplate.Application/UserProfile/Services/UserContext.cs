using WebApiTemplate.Application.UserProfile.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebApiTemplate.Application.UserProfile.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool IsAuthorized()
        {
            return _contextAccessor.HttpContext.User != null;
        }

        public int UserId
        {
            get
            {
                var user = _contextAccessor.HttpContext.User;
                if (user != null && !string.IsNullOrEmpty(user.Identity.Name))
                {
                    return int.Parse(user.Identity.Name);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
