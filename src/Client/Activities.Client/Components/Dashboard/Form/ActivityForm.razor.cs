using Activities.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard.Form
{
    public partial class ActivityForm
    {
        private ActivityDto? _activityDto;

        [Parameter]
        public ActivityDto? Activity 
        { 
            get => _activityDto; 
            set
            {
                if (_activityDto == value) 
                    return;

                _activityDto = value;
                ActivityChanged.InvokeAsync(value);
            } 
        }

        [Parameter]
        public EventCallback<ActivityDto?> ActivityChanged { get; set; }

        private bool _editMode = false;

        [Parameter]
        public bool EditMode
        { 
            get => _editMode; 
            set
            {
                if (_editMode == value)
                    return;

                _editMode = value;
                EditModeChanged.InvokeAsync(value);
            } 
        }

        [Parameter]
        public EventCallback<bool> EditModeChanged { get; set; }

        private bool _createMode = false;

        [Parameter]
        public bool CreateMode
        { 
            get => _createMode; 
            set
            {
                if (_createMode == value)
                    return;

                _createMode = value;
                CreateModeChanged.InvokeAsync(value);
            } 
        }

        [Parameter]
        public EventCallback<bool> CreateModeChanged { get; set; }

        private void Cancel()
        {
            CreateMode = false;
            EditMode = false;
        }
    }
}
