namespace FrankIS.MyReporter.MobileClient.Models;

public record ReportTask : SqliteEntity
{
    public string Title { get; set; } = "New task title";
    public string? Description { get; set; }
    public string? Url { get; set; }
    public bool Permanent { get; set; }
}
