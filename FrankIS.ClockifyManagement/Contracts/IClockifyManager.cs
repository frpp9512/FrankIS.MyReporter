using FrankIS.ClockifyManagement.DTO;
using FrankIS.ClockifyManagement.Models;

namespace FrankIS.ClockifyManagement.Contracts;

public interface IClockifyManager
{
    Task<ClockifyUserInfo?> GetUserInfo();
    Task<List<ClockifyWorkspace>?> GetWorkspaces();
    Task<ClockifyProject?> GetProject(string workspaceId, string projectId);
    Task<List<ClockifyProject>?> GetProjects(string workspaceId, int page = 1, int pageSize = 50);
    Task<ClockifyTag?> GetTag(string workspaceId, string tagId);
    Task<List<ClockifyTag>?> GetTags(string workspaceId);
    Task<ClockifyTimeEntry?> AddNewTimeEntry(string workspaceId, CreateClockifyTimeEntryDto newTimeEntry);
    Task<ClockifyTimeEntry?> AddNewTimeEntry(string workspaceId, string userId, CreateClockifyTimeEntryDto newTimeEntry);
    Task<ClockifyTimeEntry?> GetTimeEntry(string workspaceId, string id);
    Task<List<ClockifyTimeEntry>?> GetTimeEntries(string workspaceId, string userId, DateTime? start = null, DateTime? end = null, int page = 1, int pageSize = 50);
    Task<ClockifyWorkspace?> GetWorkspace(string workspaceId);
}
