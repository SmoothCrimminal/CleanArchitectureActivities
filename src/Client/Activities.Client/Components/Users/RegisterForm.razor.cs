using Activities.Client.Auth;
using Activities.Client.ViewModels;
using Activities.Models.Dtos;
using Activities.Services.Account;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Activities.Client.Components.Users
{
    public partial class RegisterForm
    {
        [Inject]
        AccountService AccountService { get; set; }

        [Inject]
        ILocalStorageService LocalStorageService { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; } = new();

        private bool _isBusy = false;

        private async Task RegisterAsync()
        {
            _isBusy = true;

            if (AccountService is null)
                throw new Exception("Account service was not injected");

            var result = await AccountService.RegisterAsync((RegisterDto)RegisterViewModel);
            if (result.Failed || result.Payload is null)
            {
                _isBusy = false;
                SnackBar.Add("Register attempt failed", Severity.Error);

                return;
            }

            await LocalStorageService.SetItemAsync("jwt", result.Payload.Token);
            await AuthStateProvider.GetAuthenticationStateAsync();

            _isBusy = false;

            NavigationManager.NavigateTo("/activities");
        }
    }
}
