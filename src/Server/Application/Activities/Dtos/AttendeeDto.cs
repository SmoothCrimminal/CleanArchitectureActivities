using Domain;

namespace Application.Activities.Dtos
{
    public class AttendeeDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string? Bio { get; set; }
        public string Image { get; set; }
        public bool Following { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }

        public static explicit operator AttendeeDto(ActivityAttendee activityAttendee) => new AttendeeDto
        {
            UserName = activityAttendee.AppUser.UserName ?? string.Empty,
            DisplayName = activityAttendee.AppUser.DisplayName ?? string.Empty,
            Bio = activityAttendee.AppUser.Bio,
            Image = activityAttendee.AppUser.Photos?.FirstOrDefault(x => x.IsMain)?.Url ?? string.Empty,
            FollowersCount = activityAttendee.AppUser.Followers.Count(),
            FollowingCount = activityAttendee.AppUser.Followings.Count(),
        };
    }
}
