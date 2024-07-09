using Activities.Client.Extensions;
using Activities.Client.ViewModels;
using Activities.Models.Dtos;
using Activities.Services.Activities;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Activities.Client.Components.Dashboard.Details
{
    public partial class ActivityDetailsHeader
    {
        [Parameter, EditorRequired]
        public ActivityViewModel Activity { get; set; }

        [Parameter]
        public EventCallback<ActivityViewModel> ActivityChanged { get; set; }

        [Inject]
        public ActivitiesService ActivitiesService { get; set; } = null!;

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; } = null!;

        private bool _isLoading = false;

        private async Task UpdateAttendance()
        {
            _isLoading = true;

            if (ActivitiesService is null)
                return;

            if (AuthStateProvider is null)
                return;

            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userName = authState.GetUserName();
            if (userName is null)
                return;

            var result = await ActivitiesService.Attend(Activity.Id);
            if (result.Failed)
            {
                SnackBar.Add("Could not attend/cancel activity!", MudBlazor.Severity.Error);
                return;
            }

            if (Activity.IsGoing.HasValue && Activity.IsGoing.Value)
            {
                Activity.Attendees = Activity.Attendees.Where(atendee => atendee.UserName != userName).ToList();
                Activity.IsGoing = false;
            }
            else
            {
                var userDto = await LocalStorageService.GetItemAsync<UserDto>("user");
                if (userDto is null)
                    return;

                var attendee = new ProfileViewModel(userDto);
                Activity.Attendees.Add(attendee);
                Activity.IsGoing = true;
            }

            StateHasChanged();
            await ActivityChanged.InvokeAsync(Activity);
            _isLoading = false;
        }

        private async void CancelActivityToogle()
        {
            if (ActivitiesService is null)
                return;

            await ActivitiesService.Attend(Activity.Id);

            Activity.IsCancelled = !Activity.IsCancelled;

            StateHasChanged();
        }
    }
}
