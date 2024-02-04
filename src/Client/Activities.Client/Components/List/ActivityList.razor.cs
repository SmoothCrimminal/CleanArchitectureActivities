using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.List
{
    public partial class ActivityList
    {
        [Parameter, EditorRequired]
        public IEnumerable<ActivityViewModel>? Activities { get; set; }

        private IEnumerable<IGrouping<DateTime?, ActivityViewModel>> _activitiesGroupedByDate;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (Activities is null)
                return;

            _activitiesGroupedByDate = Activities.OrderBy(activity => activity.Date).GroupBy(activity => activity.Date);
        }
    }
}
