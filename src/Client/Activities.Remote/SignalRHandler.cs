using Activities.Interfaces.Remote;
using Activities.Models.Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace Activities.Remote
{
    public class SignalRHandler : ISignalRHandler
    {
        private HubConnection _hubConnection;

        public IList<ChatCommentDto> Comments { get; set; }


        public event EventHandler? CommentsChangedEvent;

        public async Task AddComment(string body, Guid activityId)
        {
            await _hubConnection.InvokeAsync("SendComment", new AddCommentDto(body, activityId));
        }

        public async Task CreateHubConnection(Guid activityId, string? token)
        {
            if (string.IsNullOrWhiteSpace(activityId.ToString()))
                return;

            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:7048/chat?activityId={activityId}", opt =>
                {
                    opt.AccessTokenProvider = () => Task.FromResult(token);
                })
                .WithAutomaticReconnect()
                .ConfigureLogging(x => x.SetMinimumLevel(LogLevel.Information))
                .Build();

            await _hubConnection.StartAsync();

            _hubConnection.On<IEnumerable<ChatCommentDto>>("LoadComments", comments =>
            {
                Comments = comments.ToList();
                CommentsChangedEvent?.Invoke(null, EventArgs.Empty);
            });

            _hubConnection.On<ChatCommentDto>("ReceiveComment", comment =>
            {
                Comments.Add(comment);
                CommentsChangedEvent?.Invoke(null, EventArgs.Empty);
            });
        }

        public async Task Disconnect()
        {
            await _hubConnection.StopAsync();
            Comments.Clear();
        }
    }

    public record AddCommentDto(string Body, Guid ActivityId);
}
