using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard
{
    public partial class ActivityDashboard
    {
        [Parameter, EditorRequired]
        public IEnumerable<ActivityViewModel>? Activities { get; set; }
    }
}
