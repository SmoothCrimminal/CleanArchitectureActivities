using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries
{
    public record GetActivitiesQuery() : IRequest<List<Activity>> { }

    public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, List<Activity>>
    {
        private readonly DataContext _dataContext;

        public GetActivitiesQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Activity>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
            => await _dataContext.Activities.ToListAsync();
    }
}
