using Activities.Client.ViewModels;
using Activities.Models.Shared;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard
{
    public partial class ActivityDashboard
    {
        [Parameter, EditorRequired]
        public IEnumerable<ActivityViewModel>? Activities { get; set; }

        [Parameter]
        public ActivityParams ActivityParams { get; set; }

        [Parameter]
        public EventCallback<ActivityParams> ActivityParamsChanged { get; set; }

        private async Task DateFilterChanged(DateTime? dateTime)
        {
            if (!dateTime.HasValue) 
                return;

            ActivityParams.StartDate = dateTime.Value;
            await ActivityParamsChanged.InvokeAsync(ActivityParams);
        }

        private async Task ActivityFilterChanged(string activityFilter)
        {
            if (string.IsNullOrWhiteSpace(activityFilter))
                return;

            if (activityFilter == "all")
            {
                ActivityParams.IsGoing = false;
                ActivityParams.IsHost = false;
            }

            if (activityFilter == "isGoing")
            {
                ActivityParams.IsGoing = true;
                ActivityParams.IsHost = false;
            }

            if (activityFilter == "isHost")
            {
                ActivityParams.IsGoing = false;
                ActivityParams.IsHost = true;
            }

            await ActivityParamsChanged.InvokeAsync(ActivityParams);
        }
    }
}
