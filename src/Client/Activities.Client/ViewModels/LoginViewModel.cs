using Activities.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Activities.Client.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? DisplayName { get; set; }
        public string? UserName { get; set; }

        public static explicit operator LoginDto(LoginViewModel model) => new LoginDto
        {
            Email = model.Login,
            Password = model.Password,
        };

        public static explicit operator LoginViewModel(UserDto userDto) => new LoginViewModel
        {
            DisplayName = userDto.DisplayName,
            UserName = userDto.Username
        };
    }
}
