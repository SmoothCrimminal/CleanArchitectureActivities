using Activities.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Activities.Client.ViewModels
{
    public class ProfileViewModel
    {
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string DisplayName { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Bio { get; set; }
        public bool Following { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public ICollection<PhotoViewModel>? Photos { get; set; }

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
            Photos = profileDto.Photos?.Select(x => (PhotoViewModel)x).ToList(),
            FollowersCount = profileDto.FollowersCount,
            FollowingCount = profileDto.FollowingCount,
            Following = profileDto.Following,
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
