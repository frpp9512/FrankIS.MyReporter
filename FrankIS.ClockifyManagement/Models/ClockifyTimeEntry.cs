namespace FrankIS.ClockifyManagement.Models;

public record ClockifyTimeEntry
{
    public bool Billable { get; set; }
    public ClockifyCustomFieldValue[]? CustomFieldValues { get; set; }
    public string? Description { get; set; }
    public required string Id { get; set; }
    public bool IsLocked { get; set; }
    public string? KioskId { get; set; }
    public string? ProjectId { get; set; }
    public string[] TagIds { get; set; } = [];
    public required string TaskId { get; set; }
    public required ClockifyTimeEntryTimeInterval TimeInterval { get; set; }
    public required string Type { get; set; }
    public required string UserId { get; set; }
    public required string WorkspaceId { get; set; }
}
