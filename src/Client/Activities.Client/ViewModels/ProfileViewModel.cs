using Activities.Models.Dtos;

namespace Activities.Client.ViewModels
{
    public class ProfileViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Bio { get; set; }

        public ProfileViewModel()
        {
            
        }

        public ProfileViewModel(UserDto userDto)
        {
            UserName = userDto.Username ?? string.Empty;
            DisplayName = userDto.DisplayName ?? string.Empty;
            Image = userDto.Image;
        }

        public static explicit operator ProfileViewModel(ProfileDto profileDto) => new ProfileViewModel
        {
            Bio = profileDto.Bio,
            DisplayName = profileDto.DisplayName,
            Image = profileDto.Image,
            UserName = profileDto.UserName,
        };

        public static explicit operator ProfileDto(ProfileViewModel profileViewModel) => new ProfileDto
        {
            Bio = profileViewModel.Bio,
            DisplayName = profileViewModel.DisplayName,
            Image = profileViewModel.Image,
            UserName = profileViewModel.UserName,
        };
    }
}
