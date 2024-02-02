using Activities.Client.ViewModels;
using Activities.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard
{
    public partial class ActivityList
    {
        [Parameter, EditorRequired]
        public IEnumerable<ActivityViewModel>? Activities { get; set; }

        [Parameter]
        public ActivityViewModel? SelectedActivity { get; set; }

        [Parameter]
        public EventCallback<ActivityViewModel?> SelectedActivityChanged { get; set; }

        private async Task SelectActivity(ActivityViewModel activity)
        {
            SelectedActivity = activity;
            await SelectedActivityChanged.InvokeAsync(activity);
        }

        private async Task Delete(ActivityViewModel activity)
        {
            Activities = Activities?.Where(x => x.Id != activity.Id).ToList();

            await ActivitiesService.DeleteActivity(activity.Id);
            StateHasChanged();
        }
    }
}
