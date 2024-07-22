namespace Activities.Models.Dtos
{
    public class UpdateProfileDto
    {
        public string DisplayName { get; set; } = string.Empty;
        public string? Bio { get; set; }

        public UpdateProfileDto(string displayName, string? bio)
        {
            DisplayName = displayName;
            Bio = bio;
        }
    }
}
