namespace Activities.Models.Dtos
{
    public class ProfileDto
    {
        public string UserName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Bio { get; set; }
        public ICollection<PhotoDto>? Photos { get; set; }

        public ProfileDto()
        {
            
        }

        public ProfileDto(UserDto userDto)
        {
            UserName = userDto.Username;
            DisplayName = userDto.DisplayName;
            Image = userDto.Image;
        }
    }
}
