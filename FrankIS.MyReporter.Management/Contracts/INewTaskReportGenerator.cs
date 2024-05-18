using FrankIS.MyReporter.Management.Models;

namespace FrankIS.MyReporter.Management.Contracts;
public interface INewTaskReportGenerator
{
    CreateTaskReport GenerateTaskReport(string taskDescription, int? reportTime = null);
}
