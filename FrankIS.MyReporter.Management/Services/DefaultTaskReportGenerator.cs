using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Management.Models;
using System.Text.RegularExpressions;

namespace FrankIS.MyReporter.Management.Services;

public partial class DefaultNewTaskReportGenerator : INewTaskReportGenerator
{
    public CreateTaskReport GenerateTaskReport(string taskDescription, int? reportTime = null)
    {
        Match taskMatch = TaskNameRegex().Match(taskDescription);
        return !taskMatch.Success
            ? new()
            {
                TaskDescription = taskDescription,
                TaskName = taskDescription,
                ReportTime = reportTime
            }
            : new()
            {
                TaskDescription = taskDescription,
                TaskName = taskMatch.Groups["task"].Value,
                DevTime = int.Parse(taskMatch.Groups["dev"].Value),
                CodeReviewTime = int.Parse(taskMatch.Groups["code_review"].Value),
                QATime = int.Parse(taskMatch.Groups["qa"].Value),
                ReportTime = reportTime
            };
    }

    [GeneratedRegex(@"\((?<total>\d+)\)(?<task>.+)\[(?<dev>\d+)-(?<code_review>\d+)-(?<qa>\d+)]")]
    private static partial Regex TaskNameRegex();
}
