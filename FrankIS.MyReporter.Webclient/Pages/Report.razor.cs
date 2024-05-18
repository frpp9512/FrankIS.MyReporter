using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.Models;
using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Webclient.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace FrankIS.MyReporter.Webclient.Pages;

public partial class Report
{
    [Inject] private INewTaskReportGenerator? TaskGenerator { get; set; }
    [Inject] private ITaskReporter? TaskReporter { get; set; }
    [Inject] private IClockifyManager? ClockifyManager { get; set; }
    [Inject] private IOptions<ReportSettings>? ReportSettings { get; set; }
    [Inject] private NavigationManager? NavigationManager { get; set; }

    private string? TaskName { get; set; }
    private DateTime? From { get; set; } = DateTime.Now;
    private DateTime? To { get; set; } = DateTime.Now;
    private string? ProjectId { get; set; }
    private IEnumerable<ClockifyTag> SelectedTags { get; set; } = [];

    private List<ClockifyProject> _clockifyProjects = [];
    private List<ClockifyTag> _clockifyTags = [];
    private string? _error;
    private bool _loading = true;
    private string _taskProcessed = "";

    protected override async Task OnParametersSetAsync()
    {
        _loading = true;
        _clockifyProjects = await ClockifyManager!.GetProjectsAsync(ReportSettings!.Value.WorkspaceId, pageSize: 250) ?? [];
        _clockifyTags = await ClockifyManager!.GetTagsAsync(ReportSettings!.Value.WorkspaceId) ?? [];
        _loading = false;
    }

    private void ProcessTaskName()
    {
        if (TaskName is null)
        {
            _taskProcessed = string.Empty;
            return;
        }

        Management.Models.CreateTaskReport generatedTask = TaskGenerator!.GenerateTaskReport(TaskName);
        _taskProcessed = $"Task: {generatedTask.TaskName} with: {generatedTask.DevTime} hours for dev, {generatedTask.CodeReviewTime} hours for code review and {generatedTask.QATime} hours for QA revision.";
    }

    private async Task ReportTask()
    {
        if (To is DateTime to && From is DateTime from)
        {
            _ = Math.Ceiling(
                (DateOnly.FromDateTime(to).ToDateTime(ReportSettings!.Value.DayStartsAt) - DateOnly.FromDateTime(from).ToDateTime(ReportSettings!.Value.DayStartsAt.AddHours(ReportSettings!.Value.WorkDayDuration))).TotalDays) + 1;

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

            Management.Models.CreateTaskReport newTask = TaskGenerator!.GenerateTaskReport(TaskName);
            _ = await TaskReporter!.ReportTaskForDateRangeAsync(newTask, ProjectId, [.. SelectedTags.Select(tag => tag.Id)], DateOnly.FromDateTime(from), DateOnly.FromDateTime(to));

            NavigationManager?.NavigateTo("/");
        }
    }
}
