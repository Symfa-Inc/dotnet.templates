using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTemplate.Application.UserProfile.Interfaces;
using System.Security.Claims;
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
            var user = _contextAccessor.HttpContext.User;
            return user != null;
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
