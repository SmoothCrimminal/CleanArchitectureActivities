using Activities.Client.Extensions;
using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Profiles
{
    public partial class Profile
    {
        [Parameter, EditorRequired]
        public ProfileViewModel ProfileViewModel { get; set; } = null!;

        private void MoveToProfile()
            => NavigationManager.NavigateTo($"/profile/{ProfileViewModel.UserName}", true);

        private string DisplayBio()
        {
            if (string.IsNullOrWhiteSpace(ProfileViewModel?.Bio))
                return string.Empty;

            if (ProfileViewModel.Bio.Length > 40)
                return $"{ProfileViewModel.Bio[..40]}...";

            return ProfileViewModel.Bio;
        }
    }
}
