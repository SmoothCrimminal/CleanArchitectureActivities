using Activities.Interfaces.Remote;
using Activities.Models.Dtos;
using Activities.Models.Shared;

namespace Activities.Services.Account
{
    public class AccountService
    {
        private readonly IHttpResponseHandler _httpResponseHandler;

        public AccountService(IHttpResponseHandler httpResponseHandler)
        {
            _httpResponseHandler = httpResponseHandler;
        }

        public async Task<Result<UserDto>> GetCurrentUserAsync()
            => await _httpResponseHandler.GetAsync<UserDto>("/api/account");

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto)
            => await _httpResponseHandler.PostAsync<LoginDto, UserDto>("/api/account/login", loginDto);

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto)
            => await _httpResponseHandler.PostAsync<RegisterDto, UserDto>("/api/account/register", registerDto);
    }
}
