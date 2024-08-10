using Domain;

namespace Application.Activities.Dtos
{
    internal static class DtoExtensions
    {
        public static IEnumerable<ActivityDto> ToEnumerableDto(this IEnumerable<Activity> activities, string currentUserName)
        {
            var dtos = new List<ActivityDto>();

            foreach (var activity in activities)
            {
                var dto = (ActivityDto)activity;
                dto.Profiles = activity.Attendees.ToEnumerableDto(currentUserName).ToList();

                dtos.Add(dto);
            }

            return dtos;
        }

        public static IEnumerable<AttendeeDto> ToEnumerableDto(this IEnumerable<ActivityAttendee> attendees, string currentUserName)
        {
            var dtos = new List<AttendeeDto>();

            foreach (var atendee in attendees)
            {
                var dto = (AttendeeDto)atendee;
                dto.Following = atendee.AppUser.Followers.Any(x => x.Observer?.UserName == currentUserName);

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
