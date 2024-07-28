using Activities.Models.Dtos;

namespace Activities.Client.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Image { get; set; }

        public static explicit operator CommentViewModel(ChatCommentDto chatCommentDto) => new CommentViewModel
        {
            Id = chatCommentDto.Id,
            CreatedAt = chatCommentDto.CreatedAt,
            Body = chatCommentDto.Body,
            UserName = chatCommentDto.UserName,
            DisplayName = chatCommentDto.DisplayName,
            Image = chatCommentDto.Image,
        };
    }
}
