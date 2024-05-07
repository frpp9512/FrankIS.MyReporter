namespace FrankIS.ClockifyManagement.Models;

public record ClockifyWorkspace
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public ClockifyCostRate? CostRate { get; set; }
    public ClockifyCurrency[]? Currencies { get; set; }
    public required string FeatureSubscriptionType { get; set; }
    public string[] Features { get; set; } = [];
    public ClockifyHourlyRate? HourlyRate { get; set; }
    public string? ImageUrl { get; set; }
    public ClockifyMembership[]? Memberships { get; set; }
    public required ClockifyWorkspaceSettings WorkspaceSettings { get; set; }
}
