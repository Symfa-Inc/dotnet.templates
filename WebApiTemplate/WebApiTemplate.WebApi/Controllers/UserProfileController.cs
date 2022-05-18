using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.UserProfile.Interfaces;
using WebApiTemplate.Application.UserProfile.Models;
using System.ComponentModel.DataAnnotations;
using WebApiTemplate.WebApi.Extensions;

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
        public async Task<IActionResult> Create([Required] UserProfileCreateModel userProfileCreateModel)
        {
            var userProfileInfoModel = new UserProfileInfoModel 
            {
                UserId = User.GetId(),
                UserName = User.GetUserName(),
                Email = User.GetEmail(),
            };

            var vm = await _userProfileService.Create(userProfileInfoModel, userProfileCreateModel);
            return Ok(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vm = await _userProfileService.Get(User.GetId());
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Update([Required] UserProfileUpdateModel userProfileUpdateModel)
        {
            var vm = await _userProfileService.Update(User.GetId(), userProfileUpdateModel);
            return Ok(vm);
        }
    }
}