using Activities.Client.ViewModels;
using Activities.Models.Dtos;
using Activities.Services.Account;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Activities.Client.Components.Users
{
    public partial class LoginForm
    {
        public LoginViewModel LoginViewModel { get; set; } = new();

        [Inject]
        AccountService? AccountService { get; set; }

        [Inject]
        ILocalStorageService LocalStorageService { get; set; }

        [Inject]
        AuthenticationStateProvider AuthStateProvider { get; set; }

        private bool _isBusy = false;

        private async Task Login()
        {
            _isBusy = true;

            if (AccountService is null)
                throw new Exception("Account service was not injected");

            var result = await AccountService.LoginAsync((LoginDto)LoginViewModel);
            if (result.Failed || result.Payload is null)
            {
                _isBusy = false;
                SnackBar.Add("Login attempt failed", Severity.Error);

                return;
            }

            await LocalStorageService.SetItemAsync("jwt", result.Payload.Token);
            await AuthStateProvider.GetAuthenticationStateAsync();

            _isBusy = false;

            NavigationManager.NavigateTo("/activities");
        }
    }
}
