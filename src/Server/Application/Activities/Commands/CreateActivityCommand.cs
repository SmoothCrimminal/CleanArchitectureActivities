using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands
{
    public record CreateActivityCommand(Activity Activity) : IRequest<Result> { }

    public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand> 
    {
        public CreateActivityCommandValidator()
        {
            RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
        }
    }

    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Result>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public CreateActivityCommandHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }

        public async Task<Result> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            var attendee = new ActivityAttendee
            {
                AppUser = user,
                Activity = request.Activity,
                IsHost = true
            };

            request.Activity.Attendees.Add(attendee);

            _dataContext.Add(request.Activity);

            var savingResult = await _dataContext.SaveChangesAsync() > 0;
            if (savingResult)
                return Result.Success();

            return Result.Fail("Failed to create activity!");
        }
    }
}
