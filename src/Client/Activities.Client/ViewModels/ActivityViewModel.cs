using Activities.Client.Attributes;
using Activities.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Activities.Client.ViewModels
{
    public class ActivityViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        [FutureDate("Activity Date must be in the future")]
        public DateTime? Date { get; set; }
        public string? Description { get; set; }

        [Required]
        public string? Category { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Venue { get; set; }

        public static explicit operator ActivityDto(ActivityViewModel model) => new ActivityDto
        {
            Id = model.Id,
            Title = model.Title,
            Date = model.Date.Value,
            Description = model.Description,
            Category = model.Category,
            City = model.City,
            Venue = model.Venue,
        };

        public static explicit operator ActivityViewModel(ActivityDto dto) => new ActivityViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Date = dto.Date,
            Description = dto.Description,
            Category = dto.Category,
            City = dto.City,
            Venue = dto.Venue,
        };
    }
}
