using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.UserProfile.Interfaces;
using WebApiTemplate.Application.UserProfile.Models;
using System.ComponentModel.DataAnnotations;

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
            var vm = await _userProfileService.Create(userProfileCreateModel);
            return Ok(vm);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get([FromRoute][Required] string userId)
        {
            var vm = await _userProfileService.Get(userId);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Update([Required] UserProfileUpdateModel userProfileUpdateModel)
        {
            var vm = await _userProfileService.Update(userProfileUpdateModel);
            return Ok(vm);
        }
    }
}