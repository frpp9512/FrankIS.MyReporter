namespace FrankIS.MyReporter.Webclient.Configuration;

public record ReportSettings
{
    public required string WorkspaceId { get; set; }
    public TimeOnly DayStartsAt { get; set; }
    public int WorkDayDuration { get; set; }
    public TimeOnly LunchStarts { get; set; }
    public int LunchDuration { get; set; }
}
