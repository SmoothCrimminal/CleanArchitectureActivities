using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.List
{
    public partial class ActivityListItemAttendee
    {
        [Parameter, EditorRequired]
        public IEnumerable<ProfileViewModel> Attendees { get; set; } = new List<ProfileViewModel>();

        private bool _popupOpen = false;
    }
}
