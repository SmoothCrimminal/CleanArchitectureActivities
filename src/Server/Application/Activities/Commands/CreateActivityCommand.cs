using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands
{
    public record CreateActivityCommand(Activity Activity) : IRequest { }

    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand>
    {
        private readonly DataContext _dataContext;

        public CreateActivityCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Add(request.Activity);

            await _dataContext.SaveChangesAsync();
        }
    }
}
