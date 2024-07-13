using Application.Activities.Dtos;
using Application.Core;
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

        public GetActivitiesQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result<List<ActivityDto>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            var activities = await _dataContext.Activities
                .Include(a => a.Attendees)
                .ThenInclude(u => u.AppUser)
                .ThenInclude(p => p.Photos)
                .ToListAsync(cancellationToken);

            var dtos = activities.ToEnumerableDto().ToList();

            return Result<List<ActivityDto>>.Success(dtos);
        }
    }
}
