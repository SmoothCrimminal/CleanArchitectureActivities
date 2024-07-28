using Application.Comments.Dtos;
using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments.Commands
{
    public record CreateCommentCommand(string Body, Guid ActivityId) : IRequest<Result<CommentDto>>;

    public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.Body).NotEmpty();
        }
    }

    public class CreateCommandHandler : IRequestHandler<CreateCommentCommand, Result<CommentDto>>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public CreateCommandHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }

        public async Task<Result<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.ActivityId);
            if (activity is null)
                return Result<CommentDto>.Fail("Could not find activity");

            var user = await _dataContext.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

            var comment = new Comment
            {
                Author = user!,
                Activity = activity,
                Body = request.Body,
            };

            activity.Comments.Add(comment);
            var result = await _dataContext.SaveChangesAsync() > 0;

            if (result)
                return Result<CommentDto>.Success((CommentDto)comment);

            return Result<CommentDto>.Fail("Failed to add comment");
        }
    }
}
