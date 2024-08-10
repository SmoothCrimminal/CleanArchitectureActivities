using Activities.Interfaces.Remote;
using Activities.Models.Dtos;
using Activities.Models.Shared;

namespace Activities.Services.Profiles
{
    public class ProfilesService
    {
        private readonly IHttpResponseHandler _httpResponseHandler;

        public ProfilesService(IHttpResponseHandler httpResponseHandler)
        {
            _httpResponseHandler = httpResponseHandler;
        }

        public async Task<Result<ProfileDto>> GetProfile(string userName)
            => await _httpResponseHandler.GetAsync<ProfileDto>($"/api/profile/{userName}");

        public async Task<Result> UpdateProfile(UpdateProfileDto updateProfileDto)
            => await _httpResponseHandler.PutAsync("/api/profile", updateProfileDto);

        public async Task<Result> UploadPhoto(string base64)
            => await _httpResponseHandler.PostFileAsync("/api/photo", base64);

        public async Task<Result> SetMainPhoto(string id)
            => await _httpResponseHandler.PostAsync<object?>($"/api/photo/{id}/setMain", null);

        public async Task<Result> DeletePhoto(string id)
            => await _httpResponseHandler.DeleteAsync($"/api/photo/{id}");

        public async Task<Result> UpdateFollowing(string userName)
            => await _httpResponseHandler.PostAsync<object?>($"/api/follow/{userName}", null);

        public async Task<Result<IList<ProfileDto>>> GetFollowings(string userName, string predicate)
            => await _httpResponseHandler.GetAsync<IList<ProfileDto>>($"/api/follow/{userName}?predicate={predicate}");
    }
}
