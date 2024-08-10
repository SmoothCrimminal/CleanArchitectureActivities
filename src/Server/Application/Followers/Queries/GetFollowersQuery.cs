using Application.Core;
using Application.Interfaces;
using Application.Profiles;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Followers.Queries
{
    public record GetFollowersQuery(string Predicate, string UserName) : IRequest<Result<IList<Profile>>>;

    public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQuery, Result<IList<Profile>>>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public GetFollowersQueryHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }

        public async Task<Result<IList<Profile>>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
        {
            var profiles = new List<Profile>();

            var currentUserName = _userAccessor.GetUserName();

            profiles = request.Predicate switch
            {
                "followers" => await _dataContext.UserFollowings
                                .Include(x => x.Observer)
                                .ThenInclude(o => o.Photos)
                                .Include(x => x.Observer.Followers)
                                .Include(x => x.Observer.Followings)
                                .Where(x => x.Target.UserName == request.UserName)
                                .Select(u => CreateProfile(u.Observer, currentUserName)).ToListAsync(),

                "following" => await _dataContext.UserFollowings
                                .Include(x => x.Target)
                                .ThenInclude(x => x.Photos)
                                .Include(x => x.Target.Followers)
                                .Include(x => x.Target.Followings)   
                                .Where(x => x.Observer.UserName == request.UserName)
                                .Select(u => (Profile)u.Target).ToListAsync(),

                _ => throw new ArgumentOutOfRangeException(nameof(request.Predicate)),
            };

            return Result<IList<Profile>>.Success(profiles);
        }

        private static Profile CreateProfile(AppUser user, string currentUserName)
        {
            var profile = (Profile)user;
            profile.Following = user.Followers.Any(x => x.Observer?.UserName == currentUserName);

            return profile;
        }
    }
}
