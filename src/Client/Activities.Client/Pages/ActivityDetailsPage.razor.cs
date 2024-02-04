using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Pages
{
    public partial class ActivityDetailsPage
    {
        [Parameter]
        public Guid ActivityId { get; set; }

        public bool EditRequested { get; set; }

        private ActivityViewModel? _activityDto;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            var activityDto = await ActivitiesService.GetActivityById(ActivityId);
            if (activityDto is null)
                return;

            _activityDto = (ActivityViewModel)activityDto;
        }
    }
}
