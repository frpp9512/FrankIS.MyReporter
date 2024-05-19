using FrankIS.MyReporter.MobileClient.Pages;

namespace FrankIS.MyReporter.MobileClient.Helpers;

internal static class ConnectivityHelpers
{
    public static async Task<bool> EnsureInternetAccess(bool redirect = true)
    {
        NetworkAccess netAccess = Connectivity.Current.NetworkAccess;
        if (netAccess == NetworkAccess.Internet)
        {
            return true;
        }

        if (redirect)
        {
            await Shell.Current.GoToAsync(nameof(NoNetworkPage));
        }
        return false;
    }
}
