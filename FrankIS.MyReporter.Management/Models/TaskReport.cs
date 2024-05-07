using System.Text.RegularExpressions;

namespace FrankIS.MyReporter.Management.Models;

public record TaskReport
{
    public required string TaskDescription { get; set; }
    public required string TaskName { get; set; }
    public int DevTime { get; internal set; }
    public int CodeReviewTime { get; internal set; }
    public int QATime { get; internal set; }
    public int? ReportTime { get; set; }
}
