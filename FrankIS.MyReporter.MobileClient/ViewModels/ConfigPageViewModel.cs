using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrankIS.ClockifyManagement.Configuration;
using FrankIS.ClockifyManagement.Models;
using FrankIS.ClockifyManagement.Services;
using FrankIS.MyReporter.MobileClient.Helpers;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;

namespace FrankIS.MyReporter.MobileClient.ViewModels;
public partial class ConfigPageViewModel : ObservableObject
{
    [ObservableProperty] private string _clockifyBaseUrl = "https://api.clockify.me/api/v1/";
    [ObservableProperty] private string _clockifyApiKey = "";

    [ObservableProperty] private ObservableCollection<ClockifyWorkspace> _workspaces = [];
    [ObservableProperty] private ClockifyWorkspace? _selectedWorkspace;
    [ObservableProperty] private bool _canSelectWorkspace;
    [ObservableProperty] private bool _loadingWorkspaces;

    [ObservableProperty] private bool _displayReportConfig;
    [ObservableProperty] private bool _canSave;

    [ObservableProperty] private TimeOnly _workDayStarts = new(9, 0);
    [ObservableProperty] private int _workDayLength = 8;

    [ObservableProperty] private TimeOnly _lunchBreakStart = new(9, 0);
    [ObservableProperty] private int _lunchBreakLength = 1;

    [ObservableProperty] private string _openProjectBaseUrl = "https://project.webavanx.com/api/v3/";
    [ObservableProperty] private string _openProjectApiKey = "";

    [RelayCommand]
    public async Task LoadWorkspacesAsync()
    {
        if (string.IsNullOrEmpty(ClockifyApiKey))
        {
            await ToastHelper.ShowShortToast("Must provide an api key.");
            return;
        }

        if (string.IsNullOrEmpty(ClockifyBaseUrl))
        {
            await ToastHelper.ShowShortToast("Must provide a base url for clockify api.");
            return;
        }

        IOptions<ClockifyConfiguration> options = Options.Create(
            new ClockifyConfiguration
            {
                ApiKey = ClockifyApiKey,
                ClockifyApiBaseUrl = ClockifyBaseUrl
            });
        ClockifyManager clockifyManager = new(options);
        try
        {
            Workspaces.Clear();
            LoadingWorkspaces = true;
            CanSelectWorkspace = false;
            List<ClockifyWorkspace>? workspaces = await clockifyManager.GetWorkspacesAsync();
            if (workspaces is not null)
            {
                foreach (ClockifyWorkspace workspace in workspaces)
                {
                    Workspaces.Add(workspace);
                }
            }

            CanSelectWorkspace = true;
            DisplayReportConfig = true;
            CanSave = true;
            LoadingWorkspaces = false;
        }
        catch (Exception ex)
        {
            LoadingWorkspaces = false;
            CanSelectWorkspace = false;
            await ToastHelper.ShowShortToast($"Error while loading the workspaces. Error message: {ex.Message}");
        }
    }

    [RelayCommand]
    public async Task SaveSettingsAsync()
    {
        if (SelectedWorkspace is null)
        {
            await ToastHelper.ShowShortToast($"Must select a workspace first.");
            return;
        }

        Preferences.Set("Clockify:ApiKey", ClockifyApiKey);
        Preferences.Set("Clockify:BaseUrl", ClockifyBaseUrl);
        Preferences.Set("LunchStarts", new DateTime(DateOnly.FromDateTime(DateTime.Now), LunchBreakStart));
        Preferences.Set("LunchDuration", LunchBreakLength);
        Preferences.Set("WorkDayDuration", WorkDayLength);
        Preferences.Set("DayStartsAt", new DateTime(DateOnly.FromDateTime(DateTime.Now), WorkDayStarts));
        Preferences.Set("WorkspaceId", SelectedWorkspace.Id);
        Preferences.Set("OpenProject:BaseUrl", OpenProjectBaseUrl);
        Preferences.Set("OpenProject:ApiKey", OpenProjectApiKey);
        Preferences.Set("IsConfigured", true);
        await ToastHelper.ShowShortToast($"Restart the application to the changes to take effect.");
        await Task.Delay(1000);
        Application.Current?.Quit();
    }
}
