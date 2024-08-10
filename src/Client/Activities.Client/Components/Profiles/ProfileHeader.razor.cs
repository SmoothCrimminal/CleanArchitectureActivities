using Activities.Client.Extensions;
using Activities.Client.ViewModels;
using Activities.Services.Profiles;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Profiles
{
    public partial class ProfileHeader
    {
        [Parameter, EditorRequired]
        public ProfileViewModel Profile { get; set; }

        private bool _isCurrentUser;
        private bool _isCurrentUserFollowing;
        private string _currentUserName;
        private IList<ProfileViewModel> _currentUserFollowings;

        [Inject]
        public ProfilesService ProfilesService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            _currentUserName = authState.GetUserName();

            await LoadFollowings();

            if (_currentUserName != Profile.UserName)
                return;

            _isCurrentUser = true;
        }

        private async Task ToogleFollow(bool follow)
        {
            if (ProfilesService is null)
                return;

            var result = await ProfilesService.UpdateFollowing(Profile.UserName);
            if (result.Failed)
            {
                SnackBar.Add(result.ErrorMessage, MudBlazor.Severity.Error);
            }

            if (follow)
            {
                Profile.FollowersCount++;
            }
            else
            {
                Profile.FollowersCount--;
            }

            await LoadFollowings();
            StateHasChanged();
        }

        private async Task LoadFollowings()
        {
            if (ProfilesService is null)
                return;

            var currentUserFollowingsResult = await ProfilesService.GetFollowings(_currentUserName, "following");
            if (currentUserFollowingsResult.Failed || currentUserFollowingsResult.Payload is null)
            {
                SnackBar.Add("Could not get user followings", MudBlazor.Severity.Error);
                return;
            }

            _currentUserFollowings = currentUserFollowingsResult.Payload.Select(x => (ProfileViewModel)x).ToList();
            _isCurrentUserFollowing = _currentUserFollowings.Any(x => x.UserName == Profile.UserName);
        }
    }
}
