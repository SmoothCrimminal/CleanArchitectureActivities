using Activities.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard.Details
{
    public partial class ActivityDetails
    {
        private ActivityDto? _activityDto;

        [Parameter]
        public ActivityDto? Activity 
        { 
            get => _activityDto; 
            set
            {
                if (_activityDto == value) 
                    return;

                _activityDto = value;
                ActivityChanged.InvokeAsync(value);
            } 
        }

        [Parameter]
        public EventCallback<ActivityDto?> ActivityChanged { get; set; }

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
