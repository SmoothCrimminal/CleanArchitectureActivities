using Application.Activities.Dtos;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries
{
    public record GetActivityDetailsQuery(Guid Id) : IRequest<Result<ActivityDto>> { }

    public class GetActivityDetailsQueryHandler : IRequestHandler<GetActivityDetailsQuery, Result<ActivityDto>>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public GetActivityDetailsQueryHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }

        public async Task<Result<ActivityDto>> Handle(GetActivityDetailsQuery request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities
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
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (activity is null)
                return Result<ActivityDto>.Fail("Could not find activity");

            var activityDto = (ActivityDto)activity;
            activityDto.Profiles = activity.Attendees.ToEnumerableDto(_userAccessor.GetUserName()).ToList();

            return Result<ActivityDto>.Success(activityDto);
        }
    }
}
