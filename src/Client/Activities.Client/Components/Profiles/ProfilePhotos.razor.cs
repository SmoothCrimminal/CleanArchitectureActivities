using Activities.Client.ViewModels;
using Activities.Services.Profiles;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Profiles
{
    public partial class ProfilePhotos
    {
        [Parameter, EditorRequired]
        public ProfileViewModel Profile { get; set; }

        [Parameter]
        public EventCallback<ProfileViewModel> ProfileChanged { get; set; }

        [Parameter]
        public bool IsCurrentUser { get; set; }

        private bool _addMode;
        public bool AddMode 
        { 
            get => _addMode; 
            set
            {
                _addMode = value;
                StateHasChanged();
            }
        }

        [Inject]
        public ProfilesService ProfilesService { get; set; }

        private async Task SetMainPhoto(string photoId)
        {
            if (ProfilesService is null)
                return;

            var result = await ProfilesService.SetMainPhoto(photoId);
            if (result.Failed)
            {
                SnackBar.Add(result.ErrorMessage, MudBlazor.Severity.Error);
                return;
            }

            var profileCopy = new ProfileViewModel
            {
                Bio = Profile.Bio,
                Image = Profile.Image,
                DisplayName = Profile.DisplayName,
                Photos = Profile.Photos,
                UserName = Profile.UserName
            };

            foreach (var photo in profileCopy.Photos!)
            {
                if (photo.IsMain)
                {
                    photo.IsMain = false;
                    continue;
                }

                if (photo.Id == photoId)
                {
                    photo.IsMain = true;
                    profileCopy.Image = photo.Url;
                }
            }

            Profile = profileCopy;
            await ProfileChanged.InvokeAsync(Profile);

            StateHasChanged();
        }

        private async Task DeletePhoto(string photoId)
        {
            if (ProfilesService is null)
                return;

            var result = await ProfilesService.DeletePhoto(photoId);
            if (result.Failed)
            {
                SnackBar.Add(result.ErrorMessage, MudBlazor.Severity.Error);
                return;
            }

            Profile.Photos = Profile.Photos!.Where(p => p.Id != photoId).ToList();
            StateHasChanged();
        }
    }
}
