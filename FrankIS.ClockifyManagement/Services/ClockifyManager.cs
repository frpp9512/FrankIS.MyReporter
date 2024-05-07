using FrankIS.ClockifyManagement.Configuration;
using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.DTO;
using FrankIS.ClockifyManagement.Extensions;
using FrankIS.ClockifyManagement.Helpers;
using FrankIS.ClockifyManagement.Models;
using FrankIS.ClockifyManagement.Services.Json;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace FrankIS.ClockifyManagement.Services;
public class ClockifyManager(IOptions<ClockifyConfiguration> configuration) : IClockifyManager
{
    private const string _dateFormat = "yyyy-MM-ddThh:mm:ssZ";
    private readonly ClockifyConfiguration _clockifyConfiguration = configuration.Value;

    public async Task<ClockifyUserInfo?> GetUserInfo()
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var response = await client.GetAsync(EndpointsConstants.GetUserInfoEndpoint());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyUserInfo>();
    }

    public async Task<ClockifyTimeEntry?> AddNewTimeEntry(string workspaceId, CreateClockifyTimeEntryDto newTimeEntry)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetAddNewTimeEntryEndpoint(workspaceId);

        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        jsonOptions.Converters.Add(new ClockifyDateConverter());

        var response = await client.PostAsJsonAsync(relativeUrl, newTimeEntry, jsonOptions);
        response.EnsureSuccessStatusCode();

        var createdEntry = await response.Content.ReadFromJsonAsync<ClockifyTimeEntry>();
        return createdEntry;
    }

    public async Task<ClockifyTimeEntry?> AddNewTimeEntry(string workspaceId, string userId, CreateClockifyTimeEntryDto newTimeEntry)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetAddNewTimeEntryEndpoint(workspaceId, userId);
        
        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        jsonOptions.Converters.Add(new ClockifyDateConverter());
        
        var response = await client.PostAsJsonAsync(relativeUrl, newTimeEntry, jsonOptions);
        response.EnsureSuccessStatusCode();
        
        var createdEntry = await response.Content.ReadFromJsonAsync<ClockifyTimeEntry>();
        return createdEntry;
    }

    public async Task<ClockifyProject?> GetProject(string workspaceId, string projectId)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetProjectByIdEndpoint(workspaceId, projectId);
        var response = await client.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyProject>();
    }

    public async Task<List<ClockifyProject>?> GetProjects(string workspaceId, int page = 1, int pageSize = 50)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetProjectsEndpoint(workspaceId, page, pageSize);
        var response = await client.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ClockifyProject>>();
    }

    public async Task<ClockifyTag?> GetTag(string workspaceId, string tagId)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetTagByIdEndpoint(workspaceId, tagId);
        var response = await client.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyTag>();
    }

    public async Task<List<ClockifyTag>?> GetTags(string workspaceId)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetTagsEndpoint(workspaceId);
        var response = await client.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ClockifyTag>>();
    }

    public async Task<List<ClockifyTimeEntry>?> GetTimeEntries(string workspaceId,
                                                               string userId,
                                                               DateTime? start = null,
                                                               DateTime? end = null,
                                                               int page = 1,
                                                               int pageSize = 50)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetTimeEntriesEndpoint(workspaceId, userId);
        relativeUrl = $"{relativeUrl}?page={page}&page-size={pageSize}";
        if (start is not null)
        {
            relativeUrl = $"{relativeUrl}&start={start?.ToString(_dateFormat)}";
        }
        if (end is not null)
        {
            relativeUrl = $"{relativeUrl}&end={start?.ToString("yyyy-MM-ddThh:mm:ssZ")}";
        }

        var response = await client.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ClockifyTimeEntry>>();
    }

    public async Task<ClockifyTimeEntry?> GetTimeEntry(string workspaceId, string id)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetTimeEntryEndpoint(workspaceId, id);
        var response = await client.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyTimeEntry>();
    }

    public async Task DeleteTimeEntry(string workspaceId, string entryId)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetDeleteTimeEntryEndpoint(workspaceId, entryId);
        var response = await client.DeleteAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<ClockifyWorkspace>?> GetWorkspaces()
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetWorkspacesEndpoint();
        var response = await client.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ClockifyWorkspace>?>();
    }

    public async Task<ClockifyWorkspace?> GetWorkspace(string workspaceId)
    {
        using var client = _clockifyConfiguration.GetApiCallerClient();
        var relativeUrl = EndpointsConstants.GetWorkspaceByIdEndpoint(workspaceId);
        var response = await client.GetAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClockifyWorkspace>();
    }
}
