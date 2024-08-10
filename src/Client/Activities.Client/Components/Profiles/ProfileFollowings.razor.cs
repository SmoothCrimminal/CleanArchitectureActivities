using Activities.Client.ViewModels;
using Activities.Services.Profiles;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Profiles
{
    public partial class ProfileFollowings
    {
        [Parameter, EditorRequired]
        public ProfileViewModel Profile { get; set; }

        [Inject]
        public ProfilesService ProfilesService { get; set; }

        private IList<ProfileViewModel>? _userFollowings;

        private bool _isLoading;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            if (ProfilesService is null)
                return;

            var userFollowingsResult = await ProfilesService.GetFollowings(Profile.UserName, "following");
            if (userFollowingsResult.Failed || userFollowingsResult.Payload is null)
            {
                SnackBar.Add("Could not get user followings", MudBlazor.Severity.Error);
                return;
            }

            _userFollowings = userFollowingsResult.Payload.Select(x => (ProfileViewModel)x).ToList();

            _isLoading = false;
        }
    }
}
