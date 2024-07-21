using Activities.Models.Dtos;

namespace Activities.Client.ViewModels
{
    public class PhotoViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsMain { get; set; }

        public static explicit operator PhotoViewModel(PhotoDto photo) => new PhotoViewModel
        {
            Id = photo.Id,
            Url = photo.Url,
            IsMain = photo.IsMain,
        };
    }
}
