namespace Activities.Client.ViewModels
{
    public class ApiErrorViewModel
    {
        public string? Title { get; set; }
        public int Status { get; set; }
        public ApiError? Errors { get; set; }
    }

    public class ApiError
    {
        public string[] Id { get; set; }
    }
}
