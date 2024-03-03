using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
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

        public CreateActivityCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Add(request.Activity);

            var savingResult = await _dataContext.SaveChangesAsync() > 0;
            if (savingResult)
                return Result.Success();

            return Result.Fail("Failed to create activity!");
        }
    }
}
