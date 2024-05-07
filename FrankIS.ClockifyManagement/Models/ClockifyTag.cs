namespace FrankIS.ClockifyManagement.Models;

public record ClockifyTag
{
    public bool Archived { get; set; }
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string WorkspaceId { get; set; }
}
