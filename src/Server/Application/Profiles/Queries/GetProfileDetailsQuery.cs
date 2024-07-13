using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries
{
    public record GetProfileDetailsQuery(string UserName) : IRequest<Result<Profile>>;

    public class GetPhotoDetailsQueryHandler : IRequestHandler<GetProfileDetailsQuery, Result<Profile>>
    {
        private readonly DataContext _dataContext;

        public GetPhotoDetailsQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result<Profile>> Handle(GetProfileDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.Include(x => x.Photos).FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user is null)
                return Result<Profile>.Fail("Could not find user");

            var profile = (Profile)user;
            return Result<Profile>.Success(profile);
        }
    }
}
