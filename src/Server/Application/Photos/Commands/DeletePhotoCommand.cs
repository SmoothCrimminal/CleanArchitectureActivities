using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos.Commands
{
    public record DeletePhotoCommand(string PhotoId) : IRequest<Result>;

    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, Result>
    {
        private readonly DataContext _dataContext;

        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessor _userAccessor;

        public DeletePhotoCommandHandler(DataContext dataContext, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _photoAccessor = photoAccessor;
            _userAccessor = userAccessor;
        }

        public async Task<Result> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            if (user is null)
                return Result.Fail("Could not find user");

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.PhotoId);
            if (photo is null)
                return Result.Fail($"Could not find photo with given id: {request.PhotoId}");

            if (photo.IsMain)
                return Result.Fail("You cannot delete Your main photo");

            var result = await _photoAccessor.DeletePhoto(request.PhotoId);
            if (string.IsNullOrWhiteSpace(result))
                return Result.Fail("Problem deleting photo from cloudinary");

            user.Photos.Remove(photo);

            var success = await _dataContext.SaveChangesAsync() > 0;
            if (success)
                return Result.Success();

            return Result.Fail("Problem deleting photo from cloudinary");
        }
    }
}
