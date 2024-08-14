using Activities.Interfaces.Remote;
using Activities.Models.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace Activities.Remote
{
    public class HttpResponseHandler : IHttpResponseHandler
    {
        private readonly HttpClient _httpClient;

        public HttpResponseHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result> DeleteAsync(string endpoint)
        {
            try
            {
                var result = await _httpClient.DeleteAsync(endpoint);
                var response = await GetResultBasedOnHttpResponse(result);

                return response;
            }
            catch (Exception ex)
            {
                return Result.Fail().WithException(ex);
            }
        }

        public async Task<Result<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<T>(endpoint);
                return Result<T>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<T>.Fail().WithException(ex);
            }
        }

        public async Task<Result<PaginatedResult<T>>> GetPaginatedResult<T>(string endpoint)
        {
            try
            {
                var result = await _httpClient.GetAsync(endpoint);
                if (!result.Headers.TryGetValues("pagination", out var paginationValues))
                    throw new Exception("Could not get pagination header");

                var pagination = paginationValues.First();
                var paginationContent = JsonSerializer.Deserialize<Pagination>(pagination, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var content = await result.Content.ReadFromJsonAsync<T>();

                return Result<PaginatedResult<T>>.Success(new PaginatedResult<T>(content, paginationContent));
            }
            catch (Exception ex)
            {
                return Result<PaginatedResult<T>>.Fail().WithException(ex);
            }
        } 

        public async Task<Result> PostAsync<T>(string endpoint, T payload)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(endpoint, payload);
                var response = await GetResultBasedOnHttpResponse(result);

                return response;
            }
            catch (Exception ex)
            {
                return Result.Fail().WithException(ex);
            }
        }

        public async Task<Result> PostFileAsync(string endpoint, string base64)
        {
            try
            {
                var formData = new MultipartFormDataContent
                {
                    { new ByteArrayContent(Convert.FromBase64String(base64)), "file", $"{Guid.NewGuid()}.png" }
                };

                var result = await _httpClient.PostAsync(endpoint, formData);
                var response = await GetResultBasedOnHttpResponse(result);

                return response;
            }
            catch (Exception ex)
            {
                return Result.Fail().WithException(ex);
            }
        }

        public async Task<Result<R>> PostAsync<T, R>(string endpoint, T payload)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(endpoint, payload);
                var response = await result.Content.ReadFromJsonAsync<R>();

                return Result<R>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<R>.Fail().WithException(ex);
            }
        }

        public async Task<Result> PutAsync<T>(string endpoint, T payload)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync(endpoint, payload);
                var response = await GetResultBasedOnHttpResponse(result);

                return response;
            }
            catch (Exception ex)
            {
                return Result.Fail().WithException(ex);
            }
        }

        private async Task<Result> GetResultBasedOnHttpResponse(HttpResponseMessage? response)
        {
            if (response is null)
                return Result.Fail().WithError("Could not get response message from API call");

            if (response.IsSuccessStatusCode)
                return Result.Success();

            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return response.StatusCode switch
                {
                    System.Net.HttpStatusCode.Unauthorized => Result.Fail().WithError("Unauthorized access"),
                    System.Net.HttpStatusCode.NotFound => Result.Fail().WithError("Not found"),
                    System.Net.HttpStatusCode.BadRequest => Result.Fail().WithError("Bad request"),
                    System.Net.HttpStatusCode.InternalServerError => Result.Fail().WithError("Internal server error"),
                    _ => Result.Fail()
                };
            }

            return response.StatusCode switch
            {
                System.Net.HttpStatusCode.Unauthorized => Result.Fail().WithError("Unauthorized access"),
                System.Net.HttpStatusCode.NotFound => Result.Fail().WithError(responseContent),
                System.Net.HttpStatusCode.BadRequest => Result.Fail().WithError(responseContent),
                System.Net.HttpStatusCode.InternalServerError => Result.Fail().WithError("Internal server error").WithException(new Exception(responseContent)),
                _ => Result.Fail()
            };
        }
    }
}
