using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.MobileClient.Data;
using FrankIS.MyReporter.MobileClient.Helpers;
using FrankIS.MyReporter.MobileClient.Models;
using FrankIS.MyReporter.MobileClient.Pages;
using System.Text.RegularExpressions;

namespace FrankIS.MyReporter.MobileClient.ViewModels;

public partial class CreateOrEditPageViewModel(IOpenProjectManager openProjectManager, ReportTasksRepository repository) : ObservableObject, IQueryAttributable
{
    private readonly IOpenProjectManager _openProjectManager = openProjectManager;
    private readonly ReportTasksRepository _repository = repository;

    [ObservableProperty] private string _title = "Create new task";
    [ObservableProperty] private ReportTask _task = new();
    [ObservableProperty] private bool _loading;
    [ObservableProperty] private bool _displayForm = true;
    [ObservableProperty] private bool _isSaving;
    [ObservableProperty] private bool _displaySaveButton = true;

    [RelayCommand]
    public async Task SaveChangesAsync()
    {
        IsSaving = true;
        DisplaySaveButton = false;

        if (Task is null || string.IsNullOrEmpty(Task.Title))
        {
            await ToastHelper.ShowShortToast("Must complete the form properly.");
            IsSaving = false;
            DisplaySaveButton = true;
            return;
        }

        try
        {
            _ = await _repository.SaveItemAsync(Task);
            await ToastHelper.ShowShortToast($"Created task successfully.");
            await Shell.Current.GoToAsync($"//{nameof(TasksPage)}");
            return;
        }
        catch
        {
            await ToastHelper.ShowShortToast("Error while trying to save the changed for the task.");
        }

        IsSaving = false;
        DisplaySaveButton = true;
    }

    [RelayCommand]
    public async Task ImportFromOpenProjectAsync()
    {
        Loading = true;
        DisplayForm = false;
        DisplaySaveButton = false;

        string url = await Shell.Current.DisplayPromptAsync("Import from OpenProject", "Insert the URL of the OpenProject task", "Import", "Cancel", "Ex. https://project.webavanx.com/projects/new-sites-bfa/work_packages/8296/relations");
        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            await ToastHelper.ShowShortToast("The provided url is invalid.");
            Loading = false;
            DisplayForm = true;
            DisplaySaveButton = true;
            return;
        }

        Uri uri = new(url);
        string urlPath = uri.PathAndQuery;
        Match workspaceMatch = OpenProjectUrlPattern().Match(urlPath);
        if (!workspaceMatch.Success)
        {
            await ToastHelper.ShowShortToast("The provided url is malformed.");
            Loading = false;
            DisplayForm = true;
            DisplaySaveButton = true;
            return;
        }

        string workspaceIdString = workspaceMatch.Groups["workpackage_id"].Value;
        if (!int.TryParse(workspaceIdString, out int workspaceId))
        {
            await ToastHelper.ShowShortToast("The format of the workspace id is invalid.");
            Loading = false;
            DisplayForm = true;
            DisplaySaveButton = true;
            return;
        }

        try
        {
            Management.Models.OpenProjectResponse? workspace = await _openProjectManager.GetWorkspaceAsync(workspaceId);
            if (workspace is null)
            {
                await ToastHelper.ShowShortToast("Error while loading the OpenProject workspace.");
                Loading = false;
                DisplayForm = true;
                DisplaySaveButton = true;
                return;
            }

            ReportTask importedTask = new()
            {
                Title = workspace.Subject,
                Url = url,
                Description = $"Task imported from {url} at {DateTime.Now}"
            };

            Task = importedTask;
            Loading = false;
            DisplayForm = true;
            DisplaySaveButton = true;
            await ToastHelper.ShowShortToast("Task imported successfully.");
        }
        catch
        {
            await ToastHelper.ShowShortToast("Error while loading the OpenProject workspace.");
            Loading = false;
            DisplayForm = true;
            DisplaySaveButton = true;
            return;
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("task", out object? task) && task is ReportTask reportTask)
        {
            Title = "Edit task";
            Task = reportTask;
        }
        else
        {
            Title = "Create new task";
            Task = new();
        }

        Loading = false;
        IsSaving = false;   
        DisplayForm = true;
        DisplaySaveButton = true;
    }

    [GeneratedRegex(@"/*.*/*work_packages/(?<workpackage_id>\d+)/*.*")]
    private static partial Regex OpenProjectUrlPattern();
}
