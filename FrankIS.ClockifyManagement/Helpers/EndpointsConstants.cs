namespace FrankIS.ClockifyManagement.Helpers;

internal static class EndpointsConstants
{
    public static string GetAddNewTimeEntryEndpoint(string workspaceId, string userId)
        => $"workspaces/{workspaceId}/user/{userId}/time-entries";

    public static string GetAddNewTimeEntryEndpoint(string workspaceId)
        => $"workspaces/{workspaceId}/time-entries";

    public static string GetUserInfoEndpoint()
        => "user";

    public static string GetProjectByIdEndpoint(string workspaceId, string projectId)
        => $"workspaces/{workspaceId}/projects/{projectId}";

    public static string GetProjectsEndpoint(string workspaceId, int page = 1, int pageSize = 50)
        => $"workspaces/{workspaceId}/projects?page={page}&page-size={pageSize}";

    public static string GetTagByIdEndpoint(string workspaceId, string tagId)
        => $"workspaces/{workspaceId}/tags/{tagId}";

    public static string GetTagsEndpoint(string workspaceId)
        => $"workspaces/{workspaceId}/tags";

    public static string GetTimeEntriesEndpoint(string workspaceId, string userId)
        => $"workspaces/{workspaceId}/user/{userId}/time-entries";

    public static string GetTimeEntryEndpoint(string workspaceId, string timeEntryId)
        => $"workspaces/{workspaceId}/time-entries/{timeEntryId}";

    public static string GetDeleteTimeEntryEndpoint(string workspaceId, string timeEntryId)
        => $"workspaces/{workspaceId}/time-entries/{timeEntryId}";

    public static string GetWorkspacesEndpoint()
        => $"workspaces";

    public static string GetWorkspaceByIdEndpoint(string workspaceId)
        => $"workspaces/{workspaceId}";
}
