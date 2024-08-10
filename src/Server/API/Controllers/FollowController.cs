using Application.Followers.Commands;
using Application.Followers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class FollowController : BaseApiController
    {
        [HttpPost("{username}")]
        public async Task<IActionResult> Follow(string username)
        {
            return HandleResult(await Mediator.Send(new FollowToggleCommand(username)));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetFollowings(string username, string predicate)
        {
            return HandleResult(await Mediator.Send(new GetFollowersQuery(predicate, username)));
        }
    }
}
