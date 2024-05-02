using Application.Activities.Dtos;
using Application.Core;
using Domain;
using MediatR;
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
            var activity = await _dataContext.Activities.FindAsync(request.Id);
            var activityDto = (ActivityDto)activity;

            return Result<ActivityDto>.Success(activityDto);
        }
    }
}
