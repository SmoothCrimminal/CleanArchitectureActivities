using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Queries
{
    public record GetActivityDetailsQuery(Guid Id) : IRequest<Result<Activity>> { }

    public class GetActivityDetailsQueryHandler : IRequestHandler<GetActivityDetailsQuery, Result<Activity>>
    {
        private readonly DataContext _dataContext;

        public GetActivityDetailsQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result<Activity>> Handle(GetActivityDetailsQuery request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.Id);

            return Result<Activity>.Success(activity);
        }
    }
}
