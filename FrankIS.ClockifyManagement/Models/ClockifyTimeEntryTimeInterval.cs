namespace FrankIS.ClockifyManagement.Models;

public record ClockifyTimeEntryTimeInterval
{
    public required string Duration { get; set; }
    public TimeSpan Elapsed => End - Start;
    public DateTime End { get; set; }
    public DateTime Start { get; set; }
}
