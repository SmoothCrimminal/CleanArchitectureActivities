using Activities.Client.Extensions;
using Activities.Client.ViewModels;
using Activities.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;

namespace Activities.Client.Pages.Errors
{
    public partial class TestErrorPage
    {
        [Inject] HttpClient Client { get; set; }

        [Inject] IJSRuntime JsRuntime { get; set; }

        public void HandleNotFound()
        {
            NavigationManager.NavigateTo("/NotFound");
        }

        public async Task HandleBadRequest()
        {
            var response = await Client.GetAsync("/api/buggy/bad-request");
            await HandleApiResponseAsync(response);
        }

        public async Task HandleServerError()
        {
            var response = await Client.GetAsync("/api/buggy/server-error");

            var jsonResponse = await response.Content.ReadFromJsonAsync<ApiCriticalErrorViewModel>();
            await JsRuntime.AddItemToLocalStorage(jsonResponse, "criticalError");

            NavigationManager.NavigateTo("/ServerError");
        }

        public async Task HandleUnauthorized()
        {
            var response = await Client.GetAsync("/api/buggy/unauthorized");
            await HandleApiResponseAsync(response);
        }

        public async Task HandleBadGuid()
        {
            var response = await Client.GetAsync("/api/activities/notaguid");
            await HandleApiResponseAsync(response);
        }

        public async Task HandleValidationError()
        {
            var response = await Client.PostAsJsonAsync<ActivityDto>("/api/activities", null);
            await HandleApiResponseAsync(response);
        }   

        private async Task HandleApiResponseAsync(HttpResponseMessage? httpResponse)
        {
            if (httpResponse is null)
                return;

            SnackBar.Configuration.SnackbarVariant = Variant.Filled;

            ApiErrorViewModel? responseModel = null;

            try
            {
                responseModel = await httpResponse.Content.ReadFromJsonAsync<ApiErrorViewModel>();
            }
            catch
            {
                var responseAsString = await httpResponse.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(responseAsString))
                    return;

                SnackBar.Add(responseAsString, Severity.Error);
                return;
            }

            if (responseModel?.Errors is not null && responseModel.Errors?.Id is not null && responseModel.Errors.Id.Any())
            {
                foreach (var error in responseModel.Errors.Id)
                {
                    SnackBar.Add(error, Severity.Error);
                }
            }
            else
            {
                SnackBar.Add(responseModel?.Title, Severity.Error);
            }
        }
    }
}
