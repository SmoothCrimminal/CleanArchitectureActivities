using Activities.Client.ViewModels;
using Activities.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Dashboard.Form
{
    public partial class ActivityForm
    {
        private ActivityViewModel _activityViewModel = new();

        [Parameter]
        public ActivityViewModel Activity 
        { 
            get => _activityViewModel; 
            set
            {
                if (_activityViewModel == value) 
                    return;

                _activityViewModel = value;
                ActivityChanged.InvokeAsync(value);
            } 
        }

        [Parameter]
        public EventCallback<ActivityViewModel?> ActivityChanged { get; set; }

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

        private async Task HandleActivityEditOrCreation()
        {
            var dto = (ActivityDto)Activity;

            if (CreateMode)
            {
                dto.Id = Guid.NewGuid();

                await ActivitiesService.CreateActivity(dto);
            }
            else
                await ActivitiesService.UpdateActivity(dto);

            StateHasChanged();
        }
    }
}
