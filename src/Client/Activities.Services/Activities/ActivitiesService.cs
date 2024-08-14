using Activities.Interfaces.Remote;
using Activities.Models.Dtos;
using Activities.Models.Shared;
using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;
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

        public async Task<Result<PaginatedResult<IEnumerable<ActivityDto>>>> GetActivities(ActivityParams? @params = null)
        {
            if (@params is null)
                return await _httpResponseHandler.GetPaginatedResult<IEnumerable<ActivityDto>>("/api/activities");

            var queryBuilder = new QueryBuilder
            {
                { "pageNumber", @params.PageNumber.ToString() },
                { "pageSize", @params.PageSize.ToString() },
                { "startDate", @params.StartDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) },
                { "isHost", @params.IsHost.ToString() },
                { "isGoing", @params.IsGoing.ToString() }
            };

            return await _httpResponseHandler.GetPaginatedResult<IEnumerable<ActivityDto>>($"/api/activities{queryBuilder}");
        }

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
