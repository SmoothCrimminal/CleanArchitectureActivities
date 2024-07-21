using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Profiles
{
    public partial class ProfileContent
    {
        private ProfileViewModel _profile;

        [Parameter, EditorRequired]
        public ProfileViewModel Profile 
        {
            get => _profile;
            set
            {
                if (value is null || _profile?.Image == value.Image)
                    return;

                _profile = value;
                ProfileChanged.InvokeAsync(Profile);
            }
        }

        [Parameter]
        public EventCallback<ProfileViewModel> ProfileChanged { get; set; }

        [Parameter]
        public bool IsCurrentUser { get; set; }
    }
}
