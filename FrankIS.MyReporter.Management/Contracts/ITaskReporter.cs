using FrankIS.MyReporter.Management.Models;

namespace FrankIS.MyReporter.Management.Contracts;
public interface ITaskReporter
{
    Task<string[]> ReportTaskForDateRangeAsync(TaskReport task, string projectId, string[] tags, DateOnly start, DateOnly end);
}
