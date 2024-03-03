using Activities.Client.Extensions;
using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Activities.Client.Pages.Errors
{
    public partial class ServerError
    {
        [Inject] IJSRuntime JsRuntime { get; set; }

        private ApiCriticalErrorViewModel _apiCriticalErrorViewModel;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var result = await JsRuntime.GetItemFromLocalStorage<ApiCriticalErrorViewModel>("criticalError");
            if (result is null)
                return;

            _apiCriticalErrorViewModel = result;
        }
    }
}
