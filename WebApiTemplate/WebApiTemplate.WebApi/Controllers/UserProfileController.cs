using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.UserProfile.Interfaces;
using WebApiTemplate.Application.UserProfile.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApiTemplate.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserProfileCreateModel userProfileCreateModel)
        {
            var vm = await _userProfileService.CreateUserProfile(userProfileCreateModel);
            return Ok(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vm = await _userProfileService.GetUserProfile();
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserProfileUpdateModel userProfileUpdateModel)
        {
            var vm = await _userProfileService.UpdateUserProfile(userProfileUpdateModel);
            return Ok(vm);
        }
    }
}