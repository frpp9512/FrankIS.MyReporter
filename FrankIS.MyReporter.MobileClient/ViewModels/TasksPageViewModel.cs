using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrankIS.MyReporter.MobileClient.Data;
using FrankIS.MyReporter.MobileClient.Helpers;
using FrankIS.MyReporter.MobileClient.Models;
using FrankIS.MyReporter.MobileClient.Pages;
using System.Collections.ObjectModel;

namespace FrankIS.MyReporter.MobileClient.ViewModels;

public partial class TasksPageViewModel(ReportTasksRepository repository) : ObservableObject
{
    private readonly ReportTasksRepository _repository = repository;

    [ObservableProperty] private ObservableCollection<ReportTask> _reportTasks = [];
    [ObservableProperty] private bool _loading;
    [ObservableProperty] private bool _displayTasks;

    [RelayCommand]
    public async Task LoadTasksAsync()
    {
        Loading = true;
        DisplayTasks = false;

        ReportTasks.Clear();
        var tasks = await _repository.GetItemsAsync();
        foreach (var task in tasks)
        {
            ReportTasks.Add(task);
        }

        DisplayTasks = true;
        Loading = false;
    }

    [RelayCommand]
    public async Task CreateTaskAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreateOrEditTaskPage));
    }

    [RelayCommand]
    public async Task EditTaskAsync(ReportTask task)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "task", task }
        };

        await Shell.Current.GoToAsync(nameof(CreateOrEditTaskPage), parameters);
    }

    [RelayCommand]
    public async Task OpenLinkAsync(string url)
    {
        await Browser.OpenAsync(url);
    }

    [RelayCommand]
    public async Task ReportTaskAsync(ReportTask task)
    {
        await ToastHelper.ShowShortToast("Coming soon...");
    }

    [RelayCommand]
    public async Task ShowTaskDetailsAsync(ReportTask task)
    {
        Dictionary<string, object> parameters = new() { { "task", task } };
        await Shell.Current.GoToAsync(nameof(TaskDetailsPage), parameters);
    }

    [RelayCommand]
    public async Task RemoveTaskAsync(ReportTask task)
    {
        var response = await Shell.Current.DisplayAlert("Delete task", $"Are you sure to delete the task {task.Title}? This action will be permanent and cannot be undone.", "No", "Yes", FlowDirection.LeftToRight);
        if (response)
        {
            return;
        }

        try
        {
            await _repository.DeleteItemAsync(task);
            ReportTasks.Remove(task);
            await ToastHelper.ShowShortToast("Task removed successfully.");
        }
        catch (Exception)
        {
            await ToastHelper.ShowShortToast("Error while trying to remove the task.");
        }
    }
}
