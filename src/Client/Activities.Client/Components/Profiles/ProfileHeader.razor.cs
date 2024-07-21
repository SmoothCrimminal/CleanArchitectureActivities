using Activities.Client.Extensions;
using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Profiles
{
    public partial class ProfileHeader
    {
        [Parameter, EditorRequired]
        public ProfileViewModel Profile { get; set; }

        private bool _isCurrentUser;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userName = authState.GetUserName();

            if (userName != Profile.UserName)
                return;

            _isCurrentUser = true;
        }
    }
}
