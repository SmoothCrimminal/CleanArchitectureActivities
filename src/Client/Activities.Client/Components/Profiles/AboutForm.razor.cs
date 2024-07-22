using Activities.Client.ViewModels;
using Activities.Services.Profiles;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Profiles
{
    public partial class AboutForm
    {
        [Parameter, EditorRequired]
        public ProfileViewModel ProfileViewModel { get; set; }

        [Parameter]
        public EventCallback<ProfileViewModel> ProfileViewModelChanged { get; set; }

        [Parameter]
        public bool IsCurrentUser { get; set; }

        [Inject]
        public ProfilesService ProfilesService { get; set; }

        private ProfileViewModel? _changedProfileViewModel;
        private bool _isBusy = false;

        protected override async Task OnInitializedAsync()
        {
            RefreshChangedView();

            await base.OnInitializedAsync();
        }

        private async Task UpdateProfile()
        {
            if (ProfilesService is null)
                return;

            _isBusy = true;

            var result = await ProfilesService.UpdateProfile(new Models.Dtos.UpdateProfileDto(_changedProfileViewModel!.DisplayName, _changedProfileViewModel.Bio));
            if (result.Failed)
            {
                SnackBar.Add(result.ErrorMessage, MudBlazor.Severity.Error);
                return;
            }

            SnackBar.Add("Successfully updated profile", MudBlazor.Severity.Success);

            await ProfileViewModelChanged.InvokeAsync(_changedProfileViewModel);
            RefreshChangedView();

            _isBusy = false;
        }

        private bool CanUpdateProfile()
            => _changedProfileViewModel?.Bio == ProfileViewModel?.Bio && _changedProfileViewModel?.DisplayName == ProfileViewModel?.DisplayName;

        private void RefreshChangedView()
        {
            _changedProfileViewModel = new ProfileViewModel
            {
                Bio = ProfileViewModel.Bio,
                DisplayName = ProfileViewModel.DisplayName,
                Photos = ProfileViewModel.Photos,
                Image = ProfileViewModel.Image,
                UserName = ProfileViewModel.UserName,
            };
        }
    }
}
