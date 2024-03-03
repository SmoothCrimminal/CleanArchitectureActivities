using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries
{
    public record GetActivitiesQuery() : IRequest<Result<List<Activity>>> { }

    public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, Result<List<Activity>>>
    {
        private readonly DataContext _dataContext;

        public GetActivitiesQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result<List<Activity>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
            => Result<List<Activity>>.Success(await _dataContext.Activities.ToListAsync());
    }
}
