using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Activities.Client.Pages
{
    public partial class ActivityDetailsPage
    {
        [Parameter]
        public Guid ActivityId { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        public bool EditRequested { get; set; }

        private ActivityViewModel? _activityDto;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            var activityResult = await ActivitiesService.GetActivityById(ActivityId);
            if (activityResult.Failed)
            {
                if (activityResult.Exception is not null)
                    Snackbar.Add(activityResult.Exception.Message, Severity.Error);
                else
                    Snackbar.Add(activityResult.ErrorMessage, Severity.Error);
            }

            _activityDto = (ActivityViewModel)activityResult.Payload;
        }
    }
}
