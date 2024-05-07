namespace FrankIS.ClockifyManagement.Models;

public record ClockifyMembership
{
    public ClockifyCostRate? CostRate { get; set; }
    public ClockifyHourlyRate? HourlyRate { get; set; }
    public string? MembershipStatus { get; set; }
    public string? MembershipType { get; set; }
    public string? Currency { get; set; }
    public required string UserId { get; set; }
}
