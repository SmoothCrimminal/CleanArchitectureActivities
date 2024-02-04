using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.List
{
    public partial class ActivityListItem
    {
        [Parameter, EditorRequired]
        public ActivityViewModel Activity { get; set; }

        private void ViewActivity()
        {
            NavigationManager.NavigateTo($"/Activities/{Activity.Id}");
        }
    }
}
