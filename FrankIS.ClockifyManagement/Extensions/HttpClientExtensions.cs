using FrankIS.ClockifyManagement.Configuration;
using System.Net.Http.Headers;

namespace FrankIS.ClockifyManagement.Extensions;

internal static class HttpClientExtensions
{
    private const string _apiKeyHeader = "X-Api-Key";

    public static HttpClient GetApiCallerClient(this ClockifyConfiguration configuration)
    {
        var baseUrl = new Uri(configuration.ClockifyApiBaseUrl);
        var client = new HttpClient
        {
            BaseAddress = baseUrl
        };
        client.DefaultRequestHeaders.Add(_apiKeyHeader, configuration.ApiKey);

        return client;
    }
}
