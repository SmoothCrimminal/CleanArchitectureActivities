using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;

namespace Activities.Client.Shared
{
    public partial class InfiniteScroll
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Parameter, EditorRequired]
        public Func<Task> LoadNextData { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [JSInvokable("OnScroll")]
        public async Task JsOnScroll(int scrollY, int scrollHeight, int windowInnerHeight)
        {
            if (scrollHeight - (windowInnerHeight + scrollY) <= 0)
            {
                await LoadNewItemsAsync();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./infinite-scrolling.js");
            await JsRuntime.InvokeVoidAsync("RegisterScrollInfoService", DotNetObjectReference.Create(this));
        }

        public async Task LoadNewItemsAsync()
        {
            await LoadNextData.Invoke();
            StateHasChanged();
        }
    }
}
