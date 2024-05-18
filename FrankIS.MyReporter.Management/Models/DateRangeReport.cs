using FrankIS.ClockifyManagement.Models;

namespace FrankIS.MyReporter.Management.Models;

public record DateRangeReport
{
    public DateOnly From { get; internal set; }
    public DateOnly To { get; internal set; }
    public double HoursReported { get; internal set; }
    public double TotalDays { get; internal set; }
    public double HoursRequired { get; internal set; }
    public double CoveredPercent { get; internal set; }
    public int TaskCount { get; internal set; }
    public double ExtraTime { get; set; }
    public List<TaskReport>? TaskReports { get; internal set; }
    public List<ClockifyTimeEntry>? Entries { get; internal set; }
}
