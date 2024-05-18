using FrankIS.MyReporter.Management.Models;

namespace FrankIS.MyReporter.Management.Contracts;
public interface ITaskReporter
{
    Task<DateRangeReport> GetReportForDateRange(DateOnly from, DateOnly to);
    Task<string[]> ReportTaskForDateRangeAsync(CreateTaskReport task, string projectId, string[] tags, DateOnly start, DateOnly end);
}
