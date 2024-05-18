using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrankIS.MyReporter.MobileClient.Data;
using FrankIS.MyReporter.MobileClient.Models;
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
    public async Task ReportTaskAsync(ReportTask task)
    {

    }

    [RelayCommand]
    public async Task RemoveTaskAsync(ReportTask task)
    {

    }
}
