using FrankIS.ClockifyManagement.Models;

namespace FrankIS.MyReporter.Management.Models;

public record TaskReport
{
    public required string TaskDescription { get; set; }
    public double TotalReportedTime { get; internal set; }
    public double TotalDays { get; internal set; }
    public List<ClockifyTimeEntry>? Entries { get; internal set; }
}
