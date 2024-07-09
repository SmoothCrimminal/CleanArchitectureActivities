using Microsoft.AspNetCore.Components.Authorization;

namespace Activities.Client.Extensions
{
    public static class AuthenticationStateExtensions
    {
        public static string? GetUserName(this AuthenticationState authenticationState)
            => authenticationState.User.FindFirst("unique_name")?.Value;
    }
}
