using Application.Activities.Dtos;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries
{
    public record GetActivitiesQuery() : IRequest<Result<List<ActivityDto>>> { }

    public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, Result<List<ActivityDto>>>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public GetActivitiesQueryHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }

        public async Task<Result<List<ActivityDto>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            var activities = await _dataContext.Activities
                .Include(a => a.Attendees)
                .ThenInclude(u => u.AppUser)
                .ThenInclude(p => p.Photos)
                .Include(a => a.Attendees)
                .ThenInclude(u => u.AppUser)
                .ThenInclude(f => f.Followers)
                .ThenInclude(o => o.Observer)
                .Include(a => a.Attendees)
                .ThenInclude(u => u.AppUser)
                .ThenInclude(f => f.Followings)
                .ToListAsync(cancellationToken);

            var dtos = activities.ToEnumerableDto(_userAccessor.GetUserName()).ToList();

            return Result<List<ActivityDto>>.Success(dtos);
        }
    }
}
