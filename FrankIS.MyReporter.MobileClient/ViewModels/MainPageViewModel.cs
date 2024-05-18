using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Management.Models;
using FrankIS.MyReporter.MobileClient.Config;
using FrankIS.MyReporter.MobileClient.Helpers;
using FrankIS.MyReporter.MobileClient.Pages;

namespace FrankIS.MyReporter.MobileClient.ViewModels;
public partial class MainPageViewModel(ITaskReporter taskReporter) : ObservableObject
{
    private readonly ITaskReporter _taskReporter = taskReporter;

    [ObservableProperty] private DateOnly _from = DateOnly.FromDateTime(DateTime.Now.AddDays((int)DateTime.Now.DayOfWeek * -1));
    [ObservableProperty] private DateOnly _to = DateOnly.FromDateTime(DateTime.Now.AddDays(6 - (int)DateTime.Now.DayOfWeek));
    [ObservableProperty] private DateRangeReport? _report;
    [ObservableProperty] private bool _loadingReport;
    [ObservableProperty] private bool _enableButton;
    [ObservableProperty] private bool _displaySummary;
    [ObservableProperty] private bool _displayTaskSection;

    [RelayCommand]
    public async Task LoadReportAsync()
    {
        LoadingReport = true;
        EnableButton = false;
        DisplayTaskSection = false;
        DisplaySummary = false;

        if (!ApplicationConfig.IsConfigured)
        {
            await Shell.Current.GoToAsync(nameof(ConfigPage));
            EnableButton = true;
            LoadingReport = false;
            return;
        }

        try
        {
            Report = await _taskReporter.GetReportForDateRange(From, To);
        }
        catch (Exception ex)
        {
            EnableButton = true;
            DisplaySummary = false;
            LoadingReport = false;
            DisplayTaskSection = false;
            await ToastHelper.ShowShortToast($"Error while loading the data from Clockify. Error message: {ex.Message}");
            return;
        }

        EnableButton = true;
        LoadingReport = false;
        DisplaySummary = true;
        DisplayTaskSection = Report?.TaskReports?.Count > 0;
    }
}
