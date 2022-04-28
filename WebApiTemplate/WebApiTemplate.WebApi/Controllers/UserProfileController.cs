using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.UserProfile.Interfaces;
using WebApiTemplate.Application.UserProfile.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApiTemplate.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost]
        // TODO [Authorize]
        public async Task<IActionResult> CreateAsync(UserProfileCreateModel userProfileCreateModel)
        {
            var vm = await _userProfileService.CreateUserProfileAsync(userProfileCreateModel);
            return Ok(vm);
        }

        [HttpGet]
        // TODO [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var vm = await _userProfileService.GetUserProfileAsync();
            return Ok(vm);
        }

        [HttpPut]
        // TODO [Authorize]
        public async Task<IActionResult> UpdateAsync(UserProfileUpdateModel userProfileUpdateModel)
        {
            var vm = await _userProfileService.UpdateUserProfileAsync(userProfileUpdateModel);
            return Ok(vm);
        }
    }
}