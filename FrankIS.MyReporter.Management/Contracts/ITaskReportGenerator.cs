using FrankIS.MyReporter.Management.Models;

namespace FrankIS.MyReporter.Management.Contracts;
public interface ITaskReportGenerator
{
    TaskReport GenerateTaskReport(string taskDescription, int? reportTime = null);
}
