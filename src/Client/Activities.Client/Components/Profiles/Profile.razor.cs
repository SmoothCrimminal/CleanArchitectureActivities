using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Profiles
{
    public partial class Profile
    {
        [Parameter, EditorRequired]
        public ProfileViewModel ProfileViewModel { get; set; } = null!;

        private void MoveToProfile()
            => NavigationManager.NavigateTo($"/profile/{ProfileViewModel.UserName}");
    }
}
