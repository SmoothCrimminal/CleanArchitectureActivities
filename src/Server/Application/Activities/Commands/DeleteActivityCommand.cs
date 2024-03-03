using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities.Commands
{
    public record DeleteActivityCommand(Guid Id) : IRequest<Result?> { }

    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Result?>
    {
        private readonly DataContext _dataContext;

        public DeleteActivityCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result?> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.Id);
            if (activity is null)
                return null;

            _dataContext.Remove(activity);

            var savingResult = await _dataContext.SaveChangesAsync() > 0;
            if (savingResult)
                return Result.Success();

            return Result.Fail("Could not remove activity");
        }
    }
}
