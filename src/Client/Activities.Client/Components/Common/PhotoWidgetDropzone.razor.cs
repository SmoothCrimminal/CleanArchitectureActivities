using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Activities.Client.Components.Common
{
    public partial class PhotoWidgetDropzone
    {
        [Parameter]
        public string? FileUrl { get; set; }

        [Parameter]
        public EventCallback<string> FileUrlChanged { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        private ICollection<string> _acceptedExtensions = new List<string>() { ".jpeg", ".jpg", ".png" };

        private InputFile? _inputFile;

        private async Task OnFileChange(InputFileChangeEventArgs args)
        {
            if (Js is null)
                return;

            if (args.FileCount > 1)
            {
                SnackBar.Add("Please provide only one file!", MudBlazor.Severity.Warning);
                return;
            }

            if (!_acceptedExtensions.Contains(Path.GetExtension(args.File.Name))) 
            {
                SnackBar.Add("Plesae provide image!", MudBlazor.Severity.Warning);
                return;
            }

            var url = await Js.InvokeAsync<string>("previewImage", _inputFile!.Element);
            if (string.IsNullOrWhiteSpace(url))
                return;

            FileUrl = url;
            await FileUrlChanged.InvokeAsync(FileUrl);
        }
    }
}
