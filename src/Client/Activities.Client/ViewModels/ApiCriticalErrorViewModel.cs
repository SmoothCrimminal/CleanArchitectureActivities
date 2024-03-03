namespace Activities.Client.ViewModels
{
    public class ApiCriticalErrorViewModel
    {
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
