using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard.Details
{
    public partial class ActivityDetailsSidebar
    {
        [Parameter, EditorRequired]
        public ActivityViewModel Activity { get; set; } = new ActivityViewModel();
    }
}
