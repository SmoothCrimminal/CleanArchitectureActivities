using Activities.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard
{
    public partial class ActivityDashboard
    {
        [Parameter, EditorRequired]
        public IEnumerable<ActivityDto>? Activities { get; set; }
    }
}
