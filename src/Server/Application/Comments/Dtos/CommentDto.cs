using Domain;

namespace Application.Comments.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string? Image { get; set; }

        public static explicit operator CommentDto(Comment comment) => new CommentDto
        {
            Id = comment.Id,
            CreatedAt = comment.CreatedAt,
            Body = comment.Body,
            UserName = comment.Author.UserName ?? string.Empty,
            DisplayName = comment.Author.DisplayName ?? string.Empty,
            Image = comment.Author.Photos.FirstOrDefault(x => x.IsMain)?.Url
        };
    }
}
