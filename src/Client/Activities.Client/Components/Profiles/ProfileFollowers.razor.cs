using Activities.Client.ViewModels;
using Activities.Services.Profiles;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Profiles
{
    public partial class ProfileFollowers
    {
        [Parameter, EditorRequired]
        public ProfileViewModel Profile { get; set; }

        [Inject]
        public ProfilesService ProfilesService { get; set; }

        private IList<ProfileViewModel>? _userFollowers;

        private bool _isLoading;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            if (ProfilesService is null)
                return;

            var userFollowersResult = await ProfilesService.GetFollowings(Profile.UserName, "followers");
            if (userFollowersResult.Failed || userFollowersResult.Payload is null)
            {
                SnackBar.Add("Could not get user followers", MudBlazor.Severity.Error);
                return;
            }

            _userFollowers = userFollowersResult.Payload.Select(x => (ProfileViewModel)x).ToList();

            _isLoading = false;
        }
    }
}
