using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.DTO;
using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Management.Models;
using FrankIS.MyReporter.Webclient.Configuration;
using Microsoft.Extensions.Options;

namespace FrankIS.MyReporter.Management.Services;
public class TaskReporter(IClockifyManager clockifyManager, IOptions<ReportSettings> reportSettings) : ITaskReporter
{
    private readonly ReportSettings _settings = reportSettings.Value;
    private readonly IClockifyManager _clockifyManager = clockifyManager;

    public async Task<string[]> ReportTaskForDateRangeAsync(TaskReport task, string projectId, string[] tags, DateOnly start, DateOnly end)
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

        var addedEntries = new List<string>();
        foreach (CreateClockifyTimeEntryDto newEntry in newEntries)
        {
            var addedEntry = await _clockifyManager.AddNewTimeEntry(_settings.WorkspaceId, newEntry);
            addedEntries.Add(addedEntry!.Id);
        }

        return [.. addedEntries];
    }
}
