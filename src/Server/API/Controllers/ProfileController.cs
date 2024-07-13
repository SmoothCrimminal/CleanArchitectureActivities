using Application.Profiles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProfileController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new GetProfileDetailsQuery(username)));
        }
    }
}
