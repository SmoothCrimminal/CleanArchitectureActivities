using Activities.Models.Dtos;
using System.Net.Http.Json;

namespace Activities.Services.Activities
{
    public class ActivitiesService
    {
        private readonly HttpClient _httpClient;

        public ActivitiesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ActivityDto>?> GetActivities()
            => await _httpClient.GetFromJsonAsync<IEnumerable<ActivityDto>>("/api/activities");
    }
}
