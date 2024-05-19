using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.Models;
using FrankIS.MyReporter.MobileClient.Helpers;

namespace FrankIS.MyReporter.MobileClient.ViewModels;

public partial class UserInfoPageViewModel(IClockifyManager clockifyManager) : ObservableObject
{
    private readonly IClockifyManager _clockifyManager = clockifyManager;

    [ObservableProperty] private ClockifyUserInfo? _userInfo;
    [ObservableProperty] private ClockifyWorkspace? _workspace;
    [ObservableProperty] private bool _loading;
    [ObservableProperty] private bool _displayInfo = true;

    [RelayCommand]
    public async Task LoadUserInfoAsync()
    {
        Loading = true;
        DisplayInfo = false;

        try
        {
            UserInfo = await _clockifyManager.GetUserInfoAsync();
            Workspace = UserInfo is not null ? await _clockifyManager.GetWorkspaceAsync(UserInfo.DefaultWorkspace) : null;
        }
        catch (Exception)
        {
            await ToastHelper.ShowShortToast("Error while loading the user info");
            Loading = false;
            DisplayInfo = false;
            return;
        }

        Loading = false;
        DisplayInfo = true;
    }
}
