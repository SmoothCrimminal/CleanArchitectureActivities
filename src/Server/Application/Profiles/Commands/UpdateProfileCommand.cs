using Application.Core;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Commands
{
    public record UpdateProfileCommand(string DisplayName, string? Bio) : IRequest<Result>;

    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
        }
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public UpdateProfileCommandHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }
        
        public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUserName());
            if (user is null)
                return Result.Fail("Could not find a user");

            if (user.DisplayName == request.DisplayName && user.Bio == request.Bio)
                return Result.Fail("The data was the same");

            user.DisplayName = request.DisplayName;
            user.Bio = request.Bio;

            var result = await _dataContext.SaveChangesAsync() > 0;
            if (!result)
                return Result.Fail("Could not save changes to the database");

            return Result.Success();
        }
    }
}
