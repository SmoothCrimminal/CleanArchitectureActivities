using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard.Details
{
    public partial class ActivityDetailsInfo
    {
        [Parameter, EditorRequired]
        public ActivityViewModel Activity { get; set; }
    }
}
