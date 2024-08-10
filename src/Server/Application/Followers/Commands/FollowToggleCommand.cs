using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Runtime.InteropServices;

namespace Application.Followers.Commands
{
    public record FollowToggleCommand(string TargetUserName) : IRequest<Result>;

    public class FollowToggleCommandHandler : IRequestHandler<FollowToggleCommand, Result>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public FollowToggleCommandHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }

        public async Task<Result> Handle(FollowToggleCommand request, CancellationToken cancellationToken)
        {
            var observer = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUserName());
            if (observer is null)
                return Result.Fail("Could not find user");

            var target = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == request.TargetUserName);
            if (target is null)
                return Result.Fail("Could not find target user");

            var following = await _dataContext.UserFollowings.FindAsync(observer.Id, target.Id);
            if (following is null)
            {
                following = new Domain.UserFollowing
                {
                    Observer = observer,
                    Target = target,
                };

                _dataContext.UserFollowings.Add(following);
            }
            else
            {
                _dataContext.UserFollowings.Remove(following);
            }

            var result = await _dataContext.SaveChangesAsync() > 0;
            if (result)
                return Result.Success();

            return Result.Fail("Could not save changes to db");
        }
    }
}
