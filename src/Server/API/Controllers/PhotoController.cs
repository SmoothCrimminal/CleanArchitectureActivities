using Application.Photos.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class PhotoController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreatePhotoCommand command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new DeletePhotoCommand(id)));
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(string id)
        {
            return HandleResult(await Mediator.Send(new SetMainPhotoCommand(id)));
        }
    }
}
