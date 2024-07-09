using Activities.Client.Extensions;
using Activities.Client.ViewModels;
using Activities.Models.Dtos;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

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

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        private async Task HandleActivityEditOrCreation()
        {
            if (LocalStorageService is null)
                return;

            var currentUser = await LocalStorageService.GetItemAsync<UserDto>("user");
            if (currentUser is null)
                return;

            var dto = (ActivityDto)Activity;

            if (CreateMode)
            {
                dto.Id = Guid.NewGuid();
                dto.HostUserName = currentUser.Username;
                dto.Profiles = new List<ProfileDto>()
                {
                    new ProfileDto(currentUser)
                };

                await ActivitiesService.CreateActivity(dto);
            }
            else
                await ActivitiesService.UpdateActivity(dto);

            StateHasChanged();
        }
    }
}
