using FrankIS.ClockifyManagement.Configuration;

namespace FrankIS.ClockifyManagement.Extensions;

internal static class HttpClientExtensions
{
    private const string _apiKeyHeader = "X-Api-Key";

    public static HttpClient GetApiCallerClient(this ClockifyConfiguration configuration)
    {
        Uri baseUrl = new(configuration.ClockifyApiBaseUrl);
        HttpClient client = new()
        {
            BaseAddress = baseUrl
        };
        client.DefaultRequestHeaders.Add(_apiKeyHeader, configuration.ApiKey);

        return client;
    }
}
