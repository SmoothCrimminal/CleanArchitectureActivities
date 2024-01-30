using Activities.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard
{
    public partial class ActivityList
    {
        [Parameter, EditorRequired]
        public IEnumerable<ActivityDto>? Activities { get; set; }

        [Parameter]
        public ActivityDto? SelectedActivity { get; set; }

        [Parameter]
        public EventCallback<ActivityDto?> SelectedActivityChanged { get; set; }

        private async Task SelectActivity(ActivityDto activity)
        {
            SelectedActivity = activity;
            await SelectedActivityChanged.InvokeAsync(activity);
        }

        private void Delete(ActivityDto activity)
        {
            Activities = Activities?.Where(x => x.Id != activity.Id);
            StateHasChanged();
        }
    }
}
