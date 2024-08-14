using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard
{
    public partial class ActivityFilters
    {
        [Parameter]
        public EventCallback<DateTime?> DateChanged { get; set; }

        [Parameter]
        public EventCallback<string> ActivityFilterChanged { get; set; }

        private DateTime? _date;
        public DateTime? Date 
        { 
            get => _date; 
            set
            {
                _date = value;
                DateChanged.InvokeAsync(_date);
            } 
        }

        private bool _isExpanded;

        public async Task IsGoingFilterSelected()
            => await ActivityFilterChanged.InvokeAsync("isGoing");

        public async Task AllFilterSelected()
            => await ActivityFilterChanged.InvokeAsync("all");

        public async Task IsHostFilterSelected()
            => await ActivityFilterChanged.InvokeAsync("isHost");
    }
}
