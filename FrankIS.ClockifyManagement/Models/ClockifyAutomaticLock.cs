namespace FrankIS.ClockifyManagement.Models;

public record ClockifyAutomaticLock
{
    public string? ChangeDay { get; set; }
    public int DayOfMonth { get; set; }
    public string? FirstDay { get; set; }
    public string? OlderThanPeriod { get; set; }
    public int OlderThanValue { get; set; }
    public string? Type { get; set; }
}
