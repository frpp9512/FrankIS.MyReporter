using CommunityToolkit.Maui.Alerts;

namespace FrankIS.MyReporter.MobileClient.Helpers;

internal static class ToastHelper
{
    public static async Task ShowShortToast(string message)
    {
        var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short);
        await toast.Show();
    }
}