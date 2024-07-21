using Application.Activities.Dtos;
using Application.Core;
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

        public GetActivityDetailsQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result<ActivityDto>> Handle(GetActivityDetailsQuery request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities
                .Include(a => a.Attendees)
                .ThenInclude(u => u.AppUser)
                .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (activity is null)
                return Result<ActivityDto>.Fail("Could not find activity");

            var activityDto = (ActivityDto)activity;

            return Result<ActivityDto>.Success(activityDto);
        }
    }
}
