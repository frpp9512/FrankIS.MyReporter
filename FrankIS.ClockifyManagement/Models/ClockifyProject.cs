namespace FrankIS.ClockifyManagement.Models;

public record ClockifyProject
{
    public required string Id { get; set; }
    public required string Color { get; set; }
    public string Duration { get; set; }
    public ClockifyMembership[]? Memberships { get; set; }
    public required string Name { get; set; }
    public string? Note { get; set; }
    public bool Public { get; set; }
    public required string WorkspaceId { get; set; }
}
