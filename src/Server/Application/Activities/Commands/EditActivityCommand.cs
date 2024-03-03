using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities.Commands
{
    public record EditActivityCommand(Activity Activity) : IRequest<Result?> { }

    public class EditActivityCommandValidator : AbstractValidator<EditActivityCommand>
    {
        public EditActivityCommandValidator()
        {
            RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
        }
    }

    public class EditActivityCommandHandler : IRequestHandler<EditActivityCommand, Result?>
    {
        private readonly DataContext _dataContext;

        public EditActivityCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result?> Handle(EditActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.Activity.Id);
            if (activity is null)
                return null;

            activity.Title = string.IsNullOrWhiteSpace(request.Activity.Title) ? activity.Title : request.Activity.Title;
            activity.Category = string.IsNullOrWhiteSpace(request.Activity.Category) ? activity.Category : request.Activity.Category;
            activity.Venue = string.IsNullOrWhiteSpace(request.Activity.Venue) ? activity.Venue : request.Activity.Venue;
            activity.City = string.IsNullOrWhiteSpace(request.Activity.City) ? activity.City : request.Activity.City;
            activity.Description = string.IsNullOrWhiteSpace(request.Activity.Description) ? activity.Description : request.Activity.Description;

            var savingResult = await _dataContext.SaveChangesAsync() > 0;
            if (savingResult)
                return Result.Success();

            return Result.Fail("Could not edit activity");
        }
    }
}
