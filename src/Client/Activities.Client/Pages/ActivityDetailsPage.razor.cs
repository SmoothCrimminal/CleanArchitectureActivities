using Activities.Client.Extensions;
using Activities.Client.ViewModels;
using Activities.Models.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;

namespace Activities.Client.Pages
{
    public partial class ActivityDetailsPage
    {
        [Parameter]
        public Guid ActivityId { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        public bool EditRequested { get; set; }

        private ActivityViewModel? _activityDto;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            var activityResult = await ActivitiesService.GetActivityById(ActivityId);
            if (activityResult.Failed)
            {
                if (activityResult.Exception is not null)
                    Snackbar.Add(activityResult.Exception.Message, Severity.Error);
                else
                    Snackbar.Add(activityResult.ErrorMessage, Severity.Error);
            }

            _activityDto = (ActivityViewModel)activityResult.Payload;

            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userName = authState.GetUserName();
            if (string.IsNullOrEmpty(userName))
                return;

            _activityDto.IsGoing = _activityDto.Attendees.Any(x => x.UserName == userName && _activityDto.HostUserName != userName);
            _activityDto.IsHost = _activityDto.HostUserName == userName;
            _activityDto.Host = _activityDto.Attendees.First(x => x.UserName == _activityDto.HostUserName);
        }
    }
}
