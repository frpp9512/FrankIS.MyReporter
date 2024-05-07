using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.Models;
using Microsoft.AspNetCore.Components;

namespace FrankIS.MyReporter.Webclient.Pages;

public partial class Home
{
    [Inject] private IClockifyManager? ClockifyManager { get; set; }

    public List<ClockifyTimeEntry> TimeEntries { get; set; } = [];
    public bool _loading = true;

    protected override async Task OnParametersSetAsync()
    {
        var user = await ClockifyManager!.GetUserInfo();
        var workspaceId = "5f4ffdfee491904a9b837f42";
        TimeEntries = await ClockifyManager!.GetTimeEntries(
            workspaceId,
            user!.Id,
            start: new DateTime(2023, 1, 1),
            pageSize: 150) ?? [];
        _loading = false;
    }
}
