using Activities.Models.Shared;

namespace Activities.Interfaces.Remote
{
    public interface IHttpResponseHandler
    {
        Task<Result> PostAsync<T>(string endpoint, T payload);
        Task<Result> PostFileAsync(string endpoint, string base64);
        Task<Result<R>> PostAsync<T, R>(string endpoint, T payload);
        Task<Result> PutAsync<T>(string endpoint, T payload);
        Task<Result<T>> GetAsync<T>(string endpoint);
        Task<Result<PaginatedResult<T>>> GetPaginatedResult<T>(string endpoint);
        Task<Result> DeleteAsync(string endpoint);
    }
}
