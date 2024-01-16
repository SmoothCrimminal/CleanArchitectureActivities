using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Queries
{
    public record GetActivityDetailsQuery(Guid Id) : IRequest<Activity> { }

    public class GetActivityDetailsQueryHandler : IRequestHandler<GetActivityDetailsQuery, Activity>
    {
        private readonly DataContext _dataContext;

        public GetActivityDetailsQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Activity> Handle(GetActivityDetailsQuery request, CancellationToken cancellationToken)
            => await _dataContext.Activities.FindAsync(request.Id);
    }
}
