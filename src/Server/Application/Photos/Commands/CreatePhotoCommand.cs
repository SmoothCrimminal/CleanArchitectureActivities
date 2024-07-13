using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos.Commands
{
    public record CreatePhotoCommand(IFormFile File) : IRequest<Result<Photo>>;

    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, Result<Photo>>
    {
        private readonly DataContext _dataContext;

        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessor _userAccessor;

        public CreatePhotoCommandHandler(DataContext dataContext, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _photoAccessor = photoAccessor;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Photo>> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            if (user is null)
                return Result<Photo>.Fail("Could not find user");

            var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

            var photo = new Photo
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId
            };

            if (!user.Photos.Any(x => x.IsMain))
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            var result = await _dataContext.SaveChangesAsync() > 0;
            if (result)
                return Result<Photo>.Success(photo);

            return Result<Photo>.Fail("Problem adding photo");
        }
    }
}
