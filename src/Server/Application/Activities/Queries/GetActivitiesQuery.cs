using Application.Activities.Dtos;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries
{
    public record GetActivitiesQuery(ActivityParams Params) : IRequest<Result<PagedList<ActivityDto>>> { }

    public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, Result<PagedList<ActivityDto>>>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public GetActivitiesQueryHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }

        public async Task<Result<PagedList<ActivityDto>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            var query = _dataContext.Activities
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
                .Where(d => d.Date >= request.Params.StartDate)
                .OrderBy(d => d.Date)
                .AsQueryable();

            if (request.Params.IsGoing && !request.Params.IsHost)
            {
                query = query.Where(x => x.Attendees.Any(a => a.AppUser.UserName == _userAccessor.GetUserName() && !a.IsHost));
            }

            if (request.Params.IsHost && !request.Params.IsGoing)
            {
                query = query.Where(x => x.Attendees.Any(a => a.AppUser.UserName == _userAccessor.GetUserName() && a.IsHost));
            }

            var activities = await PagedList<Activity>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize);
            var dtos = activities.ToEnumerableDto(_userAccessor.GetUserName()).ToList();

            return Result<PagedList<ActivityDto>>.Success(new PagedList<ActivityDto>(dtos, activities.TotalCount, activities.CurrentPage, request.Params.PageSize));
        }
    }
}
