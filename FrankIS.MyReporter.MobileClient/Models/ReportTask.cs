namespace FrankIS.MyReporter.MobileClient.Models;

public record ReportTask : SqliteEntity
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string? Url { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public bool Permanent { get; set; }
}
