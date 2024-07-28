using Activities.Client.ViewModels;
using Activities.Interfaces.Remote;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard.Details
{
    public partial class ActivityDetailsChat : IDisposable
    {
        [Parameter, EditorRequired]
        public Guid ActivityId { get; set; }

        [Inject]
        ISignalRHandler SignalRHandler { get; set; }

        [Inject]
        ILocalStorageService LocalStorageService { get; set; }

        private IList<CommentViewModel> _comments = new List<CommentViewModel>();
        private string? _commentBody;

        protected override async Task OnInitializedAsync()
        {
            if (SignalRHandler is null)
                return;

            if (LocalStorageService is null)
                return;

            var token = await LocalStorageService.GetItemAsync<string>("jwt");

            await SignalRHandler.CreateHubConnection(ActivityId, token);

            SignalRHandler.CommentsChangedEvent += SignalRHandler_CommentsChangedEvent;
        }

        private void SignalRHandler_CommentsChangedEvent(object? sender, EventArgs e)
        {
            _comments = SignalRHandler.Comments.Select(x => (CommentViewModel)x).ToList();
            StateHasChanged();
        }

        public void Dispose()
        {
            SignalRHandler.CommentsChangedEvent -= SignalRHandler_CommentsChangedEvent;
            SignalRHandler.Disconnect();
        }

        public async Task AddComment()
        {
            if (SignalRHandler is null)
                return;

            if (LocalStorageService is null)
                return;

            if (string.IsNullOrWhiteSpace(_commentBody))
                return;

            await SignalRHandler.AddComment(_commentBody, ActivityId);

            _commentBody = string.Empty;
        }
    }
}
