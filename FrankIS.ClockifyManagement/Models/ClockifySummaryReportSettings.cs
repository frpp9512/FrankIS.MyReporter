namespace FrankIS.ClockifyManagement.Models;

public record ClockifySummaryReportSettings
{
    public required string Group { get; set; }
    public string? Subgroup { get; set; }
}