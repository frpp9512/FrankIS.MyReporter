using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.Models;
using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Webclient.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace FrankIS.MyReporter.Webclient.Pages;

public partial class Counter
{
    [Inject] private ITaskReportGenerator? TaskGenerator { get; set; }
    [Inject] private ITaskReporter? TaskReporter { get; set; }
    [Inject] private IClockifyManager? ClockifyManager { get; set; }
    [Inject] private IOptions<ReportSettings>? ReportSettings { get; set; }
    [Inject] private NavigationManager? NavigationManager { get; set; }

    private string? TaskName { get; set; }
    private DateOnly From { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    private DateOnly To { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    private string? ProjectId { get; set; }
    private string[]? SelectedTags { get; set; } = [];

    private List<ClockifyProject> _clockifyProjects = [];
    private List<ClockifyTag> _clockifyTags = [];
    private string? _error;
    private bool _loading = true;
    private string _taskProcessed = "";

    protected override async Task OnParametersSetAsync()
    {
        _loading = true;
        _clockifyProjects = await ClockifyManager!.GetProjects(ReportSettings!.Value.WorkspaceId, pageSize: 250) ?? [];
        _clockifyTags = await ClockifyManager!.GetTags(ReportSettings!.Value.WorkspaceId) ?? [];
        _loading = false;
    }

    private void ProcessTaskName()
    {
        if (TaskName is null)
        {
            _taskProcessed = string.Empty;
            return;
        }

        Match taskMatch = Regex.Match(TaskName, @"\((?<total>\d+)\)(?<task>.+)\[(?<dev>\d+)-(?<code_review>\d+)-(?<qa>\d+)]");
        if (!taskMatch.Success)
        {
            _taskProcessed = TaskName;
            return;
        }

        _taskProcessed = $"Task: {taskMatch.Groups["task"].Value} with: {taskMatch.Groups["dev"]} hours for dev, {taskMatch.Groups["code_review"]} hours for code review and {taskMatch.Groups["qa"]} hours for QA revision.";
    }

    private async Task ReportTask()
    {
        _ = Math.Ceiling(
            (To.ToDateTime(ReportSettings!.Value.DayStartsAt) - From.ToDateTime(ReportSettings!.Value.DayStartsAt.AddHours(ReportSettings!.Value.WorkDayDuration))).TotalDays) + 1;

        if (ProjectId == null)
        {
            _error = "You must select a project.";
            StateHasChanged();
            return;
        }

        if (string.IsNullOrEmpty(TaskName))
        {
            _error = "You must define a task name";
            return;
        }

        var newTask = TaskGenerator!.GenerateTaskReport(TaskName);
        _ = await TaskReporter!.ReportTaskForDateRangeAsync(newTask, ProjectId, [.. SelectedTags], From, To);

        NavigationManager?.NavigateTo("/");
    }
}
