using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands
{
    public record UpdateAttendanceCommand(Guid Guid) : IRequest<Result> { }

    public class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand, Result>
    {
        private readonly DataContext _dataContext;

        private readonly IUserAccessor _userAccessor;

        public UpdateAttendanceCommandHandler(DataContext dataContext, IUserAccessor userAccessor)
        {
            _dataContext = dataContext;

            _userAccessor = userAccessor;
        }

        public async Task<Result> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities
                .Include(x => x.Attendees)
                .ThenInclude(u => u.AppUser)
                .FirstOrDefaultAsync(x => x.Id == request.Guid);

            if (activity is null)
                return Result.Fail("Could not find activity with given id");

            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            if (user is null)
                return Result.Fail("Could not find a user");

            var hostUserName = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;
            if (hostUserName is null)
                return Result.Fail("Cannot get host username");

            var attendance = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

            if (attendance is not null && hostUserName == user.UserName)
                activity.IsCancelled = !activity.IsCancelled;

            if (attendance is not null && hostUserName != user.UserName)
                activity.Attendees.Remove(attendance);

            if (attendance is null)
            {
                attendance = new Domain.ActivityAttendee
                {
                    AppUser = user,
                    Activity = activity,
                    IsHost = false
                };

                activity.Attendees.Add(attendance);
            }

            var result = await _dataContext.SaveChangesAsync() > 0;

            return result ? Result.Success() : Result.Fail("Problem updating attendance");
        }
    }
}
