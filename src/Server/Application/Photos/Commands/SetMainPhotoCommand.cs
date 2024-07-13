using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos.Commands
{
    public record SetMainPhotoCommand(string PhotoId) : IRequest<Result>;

    public class SetMainPhotoCommandHandler : IRequestHandler<SetMainPhotoCommand, Result>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public SetMainPhotoCommandHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }

        public async Task<Result> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            if (user is null)
                return Result.Fail("Could not find user");

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.PhotoId);
            if (photo is null)
                return Result.Fail($"Could not find photo with id: {request.PhotoId}");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain is not null)
            {
                currentMain.IsMain = false;
            }

            photo.IsMain = true;

            var success = await _dataContext.SaveChangesAsync() > 0;
            if (success)
                return Result.Success();

            return Result.Fail("Problem setting main photo");
        }
    }
}
