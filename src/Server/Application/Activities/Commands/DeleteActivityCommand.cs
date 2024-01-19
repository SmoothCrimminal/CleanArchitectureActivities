using MediatR;
using Persistence;

namespace Application.Activities.Commands
{
    public record DeleteActivityCommand(Guid Id) : IRequest { }

    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
    {
        private readonly DataContext _dataContext;

        public DeleteActivityCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.Id);

            _dataContext.Remove(activity);

            await _dataContext.SaveChangesAsync();
        }
    }
}
