using Activities.Models.Dtos;

namespace Activities.Interfaces.Remote
{
    public interface ISignalRHandler
    {
        IList<ChatCommentDto> Comments { get; set; }

        event EventHandler? CommentsChangedEvent;

        Task CreateHubConnection(Guid activityId, string? token);

        Task Disconnect();

        Task AddComment(string body, Guid activityId);
    }
}
