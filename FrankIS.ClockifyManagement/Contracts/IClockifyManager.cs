using FrankIS.ClockifyManagement.DTO;
using FrankIS.ClockifyManagement.Models;

namespace FrankIS.ClockifyManagement.Contracts;

public interface IClockifyManager
{
    Task<ClockifyUserInfo?> GetUserInfoAsync(CancellationToken cancellationToken = default);
    Task<List<ClockifyWorkspace>?> GetWorkspacesAsync(CancellationToken cancellationToken = default);
    Task<ClockifyProject?> GetProjectAsync(string workspaceId, string projectId, CancellationToken cancellationToken = default);
    Task<List<ClockifyProject>?> GetProjectsAsync(string workspaceId, int page = 1, int pageSize = 50, CancellationToken cancellationToken = default);
    Task<ClockifyTag?> GetTagAsync(string workspaceId, string tagId, CancellationToken cancellationToken = default);
    Task<List<ClockifyTag>?> GetTagsAsync(string workspaceId, CancellationToken cancellationToken = default);
    Task<ClockifyTimeEntry?> AddNewTimeEntryAsync(string workspaceId, CreateClockifyTimeEntryDto newTimeEntry, CancellationToken cancellationToken = default);
    Task<ClockifyTimeEntry?> AddNewTimeEntryAsync(string workspaceId, string userId, CreateClockifyTimeEntryDto newTimeEntry, CancellationToken cancellationToken = default);
    Task<ClockifyTimeEntry?> GetTimeEntryAsync(string workspaceId, string id, CancellationToken cancellationToken = default);
    Task<List<ClockifyTimeEntry>?> GetTimeEntriesAsync(string workspaceId, string userId, DateTime? start = null, DateTime? end = null, int page = 1, int pageSize = 50, CancellationToken cancellationToken = default);
    Task<ClockifyWorkspace?> GetWorkspaceAsync(string workspaceId, CancellationToken cancellationToken = default);
    Task DeleteTimeEntryAsync(string workspaceId, string entryId, CancellationToken cancellationToken = default);
}
