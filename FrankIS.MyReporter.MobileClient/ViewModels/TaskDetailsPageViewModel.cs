using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrankIS.MyReporter.MobileClient.Models;
using FrankIS.MyReporter.MobileClient.Pages;

namespace FrankIS.MyReporter.MobileClient.ViewModels;

public partial class TaskDetailsPageViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private ReportTask? _task;

    [RelayCommand]
    public async Task OpenLinkAsync(string url)
    {
        await Browser.OpenAsync(url);
    }

    [RelayCommand]
    private async Task ReturnToTasksAsync()
    {
        await Shell.Current.GoToAsync($"//{nameof(TasksPage)}");
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("task", out object? taskValue) && taskValue is ReportTask task)
        {
            Task = task;
        }
        else
        {
            await ReturnToTasksAsync();
        }
    }
}
