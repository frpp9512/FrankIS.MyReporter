using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.DTO;
using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Management.Helpers;
using FrankIS.MyReporter.Management.Models;
using FrankIS.MyReporter.Webclient.Configuration;
using Microsoft.Extensions.Options;

namespace FrankIS.MyReporter.Management.Services;
public class TaskReporter(IClockifyManager clockifyManager, IOptions<ReportSettings> reportSettings) : ITaskReporter
{
    private readonly ReportSettings _settings = reportSettings.Value;
    private readonly IClockifyManager _clockifyManager = clockifyManager;
    private string? _userId;

    public async Task<string[]> ReportTaskForDateRangeAsync(CreateTaskReport task, string projectId, string[] tags, DateOnly start, DateOnly end)
    {
        double days = Math.Ceiling(
            (end.ToDateTime(_settings.DayStartsAt) - start.ToDateTime(_settings.DayStartsAt.AddHours(_settings.WorkDayDuration))).TotalDays) + 1;

        List<CreateClockifyTimeEntryDto> newEntries = [];
        for (int dayIndex = 0; dayIndex < days; dayIndex++)
        {
            DateOnly day = start.AddDays(dayIndex);
            DateTime dayStart = day.ToDateTime(_settings.DayStartsAt);
            TimeSpan launchTime = _settings.LunchStarts - _settings.DayStartsAt;
            DateTime launchStart = dayStart.AddHours(launchTime.TotalHours);
            DateTime launchEnd = launchStart.AddHours(_settings.LunchDuration);
            CreateClockifyTimeEntryDto morningEntry = new()
            {
                ProjectId = projectId,
                TagIds = [.. tags],
                Billable = false,
                Description = task.TaskDescription,
                Start = dayStart,
                End = launchStart
            };

            CreateClockifyTimeEntryDto afternoonEntry = new()
            {
                ProjectId = projectId,
                TagIds = [.. tags],
                Billable = false,
                Description = task.TaskDescription,
                Start = launchEnd,
                End = launchEnd.AddHours(_settings.WorkDayDuration - (morningEntry.End - morningEntry.Start).TotalHours)
            };

            newEntries.Add(morningEntry);
            newEntries.Add(afternoonEntry);
        }

        List<string> addedEntries = new();
        foreach (CreateClockifyTimeEntryDto newEntry in newEntries)
        {
            ClockifyManagement.Models.ClockifyTimeEntry? addedEntry = await _clockifyManager.AddNewTimeEntryAsync(_settings.WorkspaceId, newEntry);
            addedEntries.Add(addedEntry!.Id);
        }

        return [.. addedEntries];
    }

    public async Task<DateRangeReport> GetReportForDateRange(DateOnly from, DateOnly to)
    {
        string userId = await GetUserIdAsync();
        List<ClockifyManagement.Models.ClockifyTimeEntry> entries = await _clockifyManager!.GetTimeEntriesAsync(
                _settings.WorkspaceId,
                userId,
                start: new DateTime(from, TimeOnly.MinValue),
                end: new DateTime(to, TimeOnly.MaxValue),
                pageSize: 5000) ?? [];

        double hoursReported = entries.Sum(entry => (entry.TimeInterval.End - entry.TimeInterval.Start).TotalHours);
        int hoursRequired = _settings.WorkDayDuration * DateTimeHelpers.GetWeekDays(from, to).Count;
        double coveredPercent = hoursReported / hoursRequired;
        IEnumerable<IGrouping<string?, ClockifyManagement.Models.ClockifyTimeEntry>> taskGroups = entries.GroupBy(entry => entry.Description);
        int taskCount = taskGroups.Count();
        double extraDiff = hoursReported - hoursRequired;
        double totalDays = entries.Sum(entry => (entry.TimeInterval.End - entry.TimeInterval.Start).TotalHours) / _settings.WorkDayDuration;

        List<TaskReport> taskReports = taskGroups.Select(
            group => new TaskReport
            {
                TaskDescription = group.Key ?? "Task description",
                TotalDays = group.Sum(entry => (entry.TimeInterval.End - entry.TimeInterval.Start).TotalDays),
                TotalReportedTime = group.Sum(entry => (entry.TimeInterval.End - entry.TimeInterval.Start).TotalHours) / _settings.WorkDayDuration,
                Entries = [.. group]
            })
            .ToList();

        DateRangeReport report = new()
        {
            From = from,
            To = to,
            HoursReported = hoursReported,
            TotalDays = totalDays,
            HoursRequired = hoursRequired,
            CoveredPercent = coveredPercent,
            TaskCount = taskCount,
            ExtraTime = extraDiff > 0 ? extraDiff : 0,
            TaskReports = taskReports,
            Entries = entries
        };

        return report;
    }

    private async Task<string> GetUserIdAsync()
    {
        if (_userId is not null)
        {
            return _userId;
        }

        ClockifyManagement.Models.ClockifyUserInfo? user = await _clockifyManager!.GetUserInfoAsync();
        _userId = user?.Id;
        return _userId ?? throw new Exception("No user id was retrieved.");
    }
}
