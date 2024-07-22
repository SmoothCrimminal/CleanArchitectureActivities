using Application.Profiles.Commands;
using Application.Profiles.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    public class ProfileController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new GetProfileDetailsQuery(username)));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto profileDto)
        {
            return HandleResult(await Mediator.Send(new UpdateProfileCommand(profileDto.DisplayName, profileDto.Bio)));
        }
    }
}
