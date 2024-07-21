using Activities.Services.Profiles;
using Cropper.Blazor.Components;
using Cropper.Blazor.Extensions;
using Cropper.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace Activities.Client.Components.Common
{
    public partial class PhotoUploadWidget
    {
        [Inject]
        public ProfilesService ProfilesService { get; set; }

        [Parameter]
        public bool AddMode { get; set; }

        [Parameter]
        public EventCallback<bool> AddModeChanged { get; set; }

        private string? _fileUrl;
        private CropperComponent? _cropperComponent;
        private bool _isLoading;
        private Options _options = new Options
        {
            Preview = ".img-preview"
        };

        private GetCroppedCanvasOptions _croppedCanvasOptions = new GetCroppedCanvasOptions
        {
            MaxHeight = 500,
            MaxWidth = 500,
            ImageSmoothingQuality = ImageSmoothingQuality.High.ToEnumString()
        };

        private async Task UploadImage()
        {
            _isLoading = true;

            if (ProfilesService is null)
            {
                _isLoading = false;
                _fileUrl = null;

                return;
            }

            var croppedImage = await _cropperComponent!.GetCroppedCanvasDataURLAsync(_croppedCanvasOptions);
            if (string.IsNullOrWhiteSpace(croppedImage))
            {
                _isLoading = false;
                _fileUrl = null;

                return;
            }

            var decodedImage = croppedImage.Decode();
            if (string.IsNullOrWhiteSpace(decodedImage.base64ImageData))
            {
                _isLoading = false;
                _fileUrl = null;

                return;
            }

            var response = await ProfilesService.UploadPhoto(decodedImage.base64ImageData);
            if (response.Failed)
            {
                SnackBar.Add(response.ErrorMessage, MudBlazor.Severity.Error);
                _isLoading = false;
                _fileUrl = null;

                return;
            }

            SnackBar.Add("Successfully uploaded image", MudBlazor.Severity.Success);

            _isLoading = false;
            _fileUrl = null;
            AddMode = false;
            await AddModeChanged.InvokeAsync(AddMode);
        }

        private void CancelFile()
        {
            _fileUrl = null;
        }
    }
}
