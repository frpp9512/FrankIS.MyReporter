using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.Models;
using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Management.Models;
using FrankIS.MyReporter.Webclient.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace FrankIS.MyReporter.Webclient.Pages;

public partial class Home
{
    [Inject] private ITaskReporter? TaskReporter { get; set; }
    [Inject] private IClockifyManager? ClockifyManager { get; set; }
    [Inject] private IOptions<ReportSettings>? ReportSettings { get; set; }

    private (DateOnly from, DateOnly to)? DateRange { get; set; } = (DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now));
    private DateOnly? SelectedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    private DateRangeReport? GeneratedReport { get; set; }

    private readonly string _workspaceId = "5f4ffdfee491904a9b837f42";
    public IQueryable<ClockifyTimeEntry>? TimeEntries { get; set; }
    public bool _loading;
    private bool _error;

    protected override async Task OnParametersSetAsync()
    {
        await LoadEntriesAsync();
    }

    private async Task LoadEntriesAsync()
    {
        if (_loading)
        {
            return;
        }

        if (DateRange is not (DateOnly, DateOnly) range)
        {
            return;
        }

        _error = false;
        _loading = true;
        try
        {
            var report = await TaskReporter!.GetReportForDateRange(range.from, range.to);
            GeneratedReport = report;
            TimeEntries = report.Entries?.AsQueryable();
        }
        catch (Exception)
        {
            _error = true;
        }

        _loading = false;
    }

    private async Task RemoveEntry(string entryId)
    {
        await ClockifyManager!.DeleteTimeEntryAsync(_workspaceId, entryId);
        await LoadEntriesAsync();
        StateHasChanged();
    }

    private async Task SelectedRangeChanged((DateOnly from, DateOnly to)? range)
    {
        DateRange = range;
        await LoadEntriesAsync();
        StateHasChanged();
    }
}
