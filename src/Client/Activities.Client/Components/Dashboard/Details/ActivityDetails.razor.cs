using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard.Details
{
    public partial class ActivityDetails
    {
        private ActivityViewModel? _activityViewModel;

        [Parameter]
        public ActivityViewModel? Activity 
        { 
            get => _activityViewModel; 
            set
            {
                if (_activityViewModel == value) 
                    return;

                _activityViewModel = value;
                ActivityChanged.InvokeAsync(value);
            } 
        }

        [Parameter]
        public EventCallback<ActivityViewModel?> ActivityChanged { get; set; }

        [Parameter]
        public bool EditRequested { get; set; }

        [Parameter]
        public EventCallback<bool> EditRequestedChanged { get; set; }

        private void Edit()
        {
            EditRequested = true;
            EditRequestedChanged.InvokeAsync(EditRequested);
        }

        private void Cancel()
        {
            EditRequested = false;
            EditRequestedChanged.InvokeAsync(EditRequested);

            Activity = null;
        }
    }
}
