using Activities.Client.ViewModels;
using Activities.Models.Dtos;
using Activities.Services.Profiles;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Pages
{
    public partial class ProfilePage
    {
        [Parameter]
        public string Username { get; set; }

        [Inject]
        ProfilesService ProfilesService { get; set; }

        private ProfileViewModel _profileViewModel;

        private bool _isLoading;

        protected override async Task OnInitializedAsync()
        {
            await LoadProfile();
        }

        private async Task LoadProfile()
        {
            _isLoading = true;

            if (ProfilesService is null)
                return;

            var profileResult = await ProfilesService.GetProfile(Username);
            if (profileResult.Failed || profileResult.Payload is null)
            {
                SnackBar.Add(profileResult.ErrorMessage, MudBlazor.Severity.Error);
                return;
            }

            _profileViewModel = (ProfileViewModel)profileResult.Payload;

            _isLoading = false;
        }
    }
}
