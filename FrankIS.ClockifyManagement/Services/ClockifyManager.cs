using FrankIS.ClockifyManagement.Configuration;
using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.DTO;
using FrankIS.ClockifyManagement.Extensions;
using FrankIS.ClockifyManagement.Helpers;
using FrankIS.ClockifyManagement.Models;
using FrankIS.ClockifyManagement.Services.Json;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace FrankIS.ClockifyManagement.Services;
public class ClockifyManager(IOptions<ClockifyConfiguration> configuration) : IClockifyManager
{
    private readonly ClockifyConfiguration _clockifyConfiguration = configuration.Value;

    public async Task<ClockifyUserInfo?> GetUserInfoAsync(CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        HttpResponseMessage response = await client.GetAsync(EndpointsConstants.GetUserInfoEndpoint(), cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyUserInfo>(cancellationToken);
    }

    public async Task<ClockifyTimeEntry?> AddNewTimeEntryAsync(string workspaceId,
                                                               CreateClockifyTimeEntryDto newTimeEntry,
                                                               CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetAddNewTimeEntryEndpoint(workspaceId);

        JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);
        jsonOptions.Converters.Add(new ClockifyDateConverter());

        HttpResponseMessage response = await client.PostAsJsonAsync(relativeUrl, newTimeEntry, jsonOptions, cancellationToken);
        _ = response.EnsureSuccessStatusCode();

        ClockifyTimeEntry? createdEntry = await response.Content.ReadFromJsonAsync<ClockifyTimeEntry>(cancellationToken);
        return createdEntry;
    }

    public async Task<ClockifyTimeEntry?> AddNewTimeEntryAsync(string workspaceId,
                                                               string userId,
                                                               CreateClockifyTimeEntryDto newTimeEntry,
                                                               CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetAddNewTimeEntryEndpoint(workspaceId, userId);

        JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);
        jsonOptions.Converters.Add(new ClockifyDateConverter());

        HttpResponseMessage response = await client.PostAsJsonAsync(relativeUrl, newTimeEntry, jsonOptions, cancellationToken);
        _ = response.EnsureSuccessStatusCode();

        ClockifyTimeEntry? createdEntry = await response.Content.ReadFromJsonAsync<ClockifyTimeEntry>(cancellationToken);
        return createdEntry;
    }

    public async Task<ClockifyProject?> GetProjectAsync(string workspaceId,
                                                        string projectId,
                                                        CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetProjectByIdEndpoint(workspaceId, projectId);
        HttpResponseMessage response = await client.GetAsync(relativeUrl, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyProject>(cancellationToken);
    }

    public async Task<List<ClockifyProject>?> GetProjectsAsync(string workspaceId,
                                                               int page = 1,
                                                               int pageSize = 50,
                                                               CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetProjectsEndpoint(workspaceId, page, pageSize);
        HttpResponseMessage response = await client.GetAsync(relativeUrl, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ClockifyProject>>(cancellationToken);
    }

    public async Task<ClockifyTag?> GetTagAsync(string workspaceId,
                                                string tagId,
                                                CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetTagByIdEndpoint(workspaceId, tagId);
        HttpResponseMessage response = await client.GetAsync(relativeUrl, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyTag>(cancellationToken);
    }

    public async Task<List<ClockifyTag>?> GetTagsAsync(string workspaceId,
                                                       CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetTagsEndpoint(workspaceId);
        HttpResponseMessage response = await client.GetAsync(relativeUrl, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ClockifyTag>>(cancellationToken);
    }

    public async Task<List<ClockifyTimeEntry>?> GetTimeEntriesAsync(string workspaceId,
                                                                    string userId,
                                                                    DateTime? start = null,
                                                                    DateTime? end = null,
                                                                    int page = 1,
                                                                    int pageSize = 50,
                                                                    CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetTimeEntriesEndpoint(workspaceId, userId);
        relativeUrl = $"{relativeUrl}?page={page}&page-size={pageSize}";
        if (start is not null)
        {
            relativeUrl = $"{relativeUrl}&start={start?.ToString(ClockifyDateConverter.DateFormat)}";
        }

        if (end is not null)
        {
            relativeUrl = $"{relativeUrl}&end={end?.ToString(ClockifyDateConverter.DateFormat)}";
        }

        HttpResponseMessage response = await client.GetAsync(relativeUrl, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ClockifyTimeEntry>>(cancellationToken);
    }

    public async Task<ClockifyTimeEntry?> GetTimeEntryAsync(string workspaceId,
                                                            string id,
                                                            CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetTimeEntryEndpoint(workspaceId, id);
        HttpResponseMessage response = await client.GetAsync(relativeUrl, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyTimeEntry>(cancellationToken);
    }

    public async Task DeleteTimeEntryAsync(string workspaceId,
                                           string entryId,
                                           CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetDeleteTimeEntryEndpoint(workspaceId, entryId);
        HttpResponseMessage response = await client.DeleteAsync(relativeUrl, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
    }

    public async Task<List<ClockifyWorkspace>?> GetWorkspacesAsync(CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetWorkspacesEndpoint();
        HttpResponseMessage response = await client.GetAsync(relativeUrl, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ClockifyWorkspace>>(cancellationToken);
    }

    public async Task<ClockifyWorkspace?> GetWorkspaceAsync(string workspaceId, CancellationToken cancellationToken = default)
    {
        using HttpClient client = _clockifyConfiguration.GetApiCallerClient();
        string relativeUrl = EndpointsConstants.GetWorkspaceByIdEndpoint(workspaceId);
        HttpResponseMessage response = await client.GetAsync(relativeUrl, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyWorkspace>(cancellationToken);
    }
}
