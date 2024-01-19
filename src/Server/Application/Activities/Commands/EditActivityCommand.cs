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
            activity.Category = string.IsNullOrWhiteSpace(request.Activity.Category) ? activity.Category : request.Activity.Category;
            activity.Venue = string.IsNullOrWhiteSpace(request.Activity.Venue) ? activity.Venue : request.Activity.Venue;
            activity.City = string.IsNullOrWhiteSpace(request.Activity.City) ? activity.City : request.Activity.City;
            activity.Description = string.IsNullOrWhiteSpace(request.Activity.Description) ? activity.Description : request.Activity.Description;

            await _dataContext.SaveChangesAsync();
        }
    }
}
