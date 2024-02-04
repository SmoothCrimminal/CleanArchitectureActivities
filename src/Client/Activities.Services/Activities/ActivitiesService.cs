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

        public async Task UpdateActivity(ActivityDto dto)
            => await _httpClient.PutAsJsonAsync($"/api/activities/{dto.Id}", dto);

        public async Task CreateActivity(ActivityDto dto)
            => await _httpClient.PostAsJsonAsync("/api/activities", dto);

        public async Task DeleteActivity(Guid id)
            => await _httpClient.DeleteAsync($"/api/activities/{id}");

        public async Task<ActivityDto?> GetActivityById(Guid id)
            => await _httpClient.GetFromJsonAsync<ActivityDto?>($"/api/activities/{id}");
    }
}
