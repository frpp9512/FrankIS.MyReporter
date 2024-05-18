using FrankIS.MyReporter.Management.Configuration;
using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Management.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace FrankIS.MyReporter.Management.Services;

public class OpenProjectManager(IOptions<OpenProjectSettings> settingsOptions) : IOpenProjectManager
{
    private readonly OpenProjectSettings _settings = settingsOptions.Value;

    public async Task<OpenProjectResponse?> GetWorkspaceAsync(int workspaceId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(_settings.BaseUrl, nameof(_settings.BaseUrl));
        ArgumentException.ThrowIfNullOrEmpty(_settings.ApiKey, nameof(_settings.ApiKey));

        string authorizationRawValue = $"apikey:{_settings.ApiKey}";
        string authorizationValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authorizationRawValue));

        using HttpClient client = new()
        {
            BaseAddress = new Uri(_settings.BaseUrl)
        };

        AuthenticationHeaderValue authorizationHeader = new("Basic", authorizationValue);
        client.DefaultRequestHeaders.Authorization = authorizationHeader;

        HttpResponseMessage response = await client.GetAsync($"work_packages/{workspaceId}", cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OpenProjectResponse>(cancellationToken);
    }
}
