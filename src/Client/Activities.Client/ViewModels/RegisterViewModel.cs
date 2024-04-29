using Activities.Client.Attributes;
using Activities.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Activities.Client.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; } = string.Empty;

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must be complex")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [NoWhiteSpaces]
        public string UserName { get; set; } = string.Empty;

        public static explicit operator RegisterDto(RegisterViewModel model) => new RegisterDto
        {
            DisplayName = model.DisplayName,
            Email = model.Login,
            Password = model.Password,
            Username = model.UserName
        };
    }
}
