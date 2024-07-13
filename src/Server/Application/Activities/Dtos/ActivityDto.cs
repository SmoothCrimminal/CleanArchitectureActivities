using Application.Profiles;
using Domain;

namespace Application.Activities.Dtos
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? City { get; set; }
        public string? Venue { get; set; }
        public string HostUserName { get; set; }
        public bool IsCancelled { get; set; }
        public ICollection<AttendeeDto> Profiles { get; set; }

        public static explicit operator ActivityDto(Activity activity) => new ActivityDto
        {
            Id = activity.Id,
            Category = activity.Category,
            City = activity.City,
            Date = activity.Date,
            Description = activity.Description,
            Title = activity.Title,
            Venue = activity.Venue,
            IsCancelled = activity.IsCancelled,
            Profiles = activity.Attendees.Select(a => (AttendeeDto)a).ToList(),
            HostUserName = activity.Attendees.FirstOrDefault(a => a.IsHost)?.AppUser?.UserName ?? string.Empty,
        };
    }
}
