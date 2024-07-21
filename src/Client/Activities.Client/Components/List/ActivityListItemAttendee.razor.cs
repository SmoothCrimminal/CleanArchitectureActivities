using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.List
{
    public partial class ActivityListItemAttendee
    {
        [Parameter, EditorRequired]
        public IEnumerable<ProfileViewModel> Attendees { get; set; } = new List<ProfileViewModel>();

        private ProfileViewModel? _currentlyViewedAttendee;

        private bool _popupOpen = false;

        private void HandleClick(ProfileViewModel attendee)
        {
            _popupOpen = !_popupOpen;
            _currentlyViewedAttendee = attendee;
        }
    }
}
