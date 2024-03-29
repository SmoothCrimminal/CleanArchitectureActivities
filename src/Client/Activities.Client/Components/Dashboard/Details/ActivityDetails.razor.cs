﻿using Activities.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard.Details
{
    public partial class ActivityDetails
    {
        [Parameter, EditorRequired]
        public ActivityViewModel Activity { get; set; }

        [Parameter]
        public bool EditRequested { get; set; }

        [Parameter]
        public EventCallback<bool> EditRequestedChanged { get; set; }
    }
}
