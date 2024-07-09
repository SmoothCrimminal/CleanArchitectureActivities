using Activities.Interfaces.Remote;
using Activities.Models.Dtos;
using Activities.Models.Shared;
using System.Net.Http.Json;

namespace Activities.Services.Activities
{
    public class ActivitiesService
    {
        private readonly IHttpResponseHandler _httpResponseHandler;

        public ActivitiesService(IHttpResponseHandler httpResponseHandler)
        {
            _httpResponseHandler = httpResponseHandler;
        }

        public async Task<Result<IEnumerable<ActivityDto>>> GetActivities()
            => await _httpResponseHandler.GetAsync<IEnumerable<ActivityDto>>("/api/activities");

        public async Task<Result> UpdateActivity(ActivityDto dto)
            => await _httpResponseHandler.PutAsync($"/api/activities/{dto.Id}", dto);

        public async Task<Result> CreateActivity(ActivityDto dto)
            => await _httpResponseHandler.PostAsync("/api/activities", dto);

        public async Task<Result> DeleteActivity(Guid id)
            => await _httpResponseHandler.DeleteAsync($"/api/activities/{id}");

        public async Task<Result<ActivityDto>> GetActivityById(Guid id)
            => await _httpResponseHandler.GetAsync<ActivityDto>($"/api/activities/{id}");

        public async Task<Result> Attend(Guid id)
            => await _httpResponseHandler.PostAsync<object?>($"/api/activities/{id}/attend", null);
    }
}
