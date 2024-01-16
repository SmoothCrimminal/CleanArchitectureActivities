using Domain;
using MediatR;
using Persistence;
using System.Runtime.InteropServices;

namespace Application.Activities.Commands
{
    public record EditActivityCommand(Activity Activity) : IRequest { }

    public class EditActivityCommandHandler : IRequestHandler<EditActivityCommand>
    {
        private readonly DataContext _dataContext;

        public EditActivityCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Handle(EditActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.Activity.Id);

            activity.Title = string.IsNullOrWhiteSpace(request.Activity.Title) ? activity.Title : request.Activity.Title;

            await _dataContext.SaveChangesAsync();
        }
    }
}
