using Domain;

namespace Application.Profiles
{
    public class Profile
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string? Bio { get; set; }
        public string Image { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public static explicit operator Profile(AppUser user) => new Profile
        {
            UserName = user.UserName,
            Bio = user.Bio,
            DisplayName = user.DisplayName,
            Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url ?? string.Empty,
            Photos = user.Photos
        };
    }
}
