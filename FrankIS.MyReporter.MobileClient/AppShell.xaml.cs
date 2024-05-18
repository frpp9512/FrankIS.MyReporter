using FrankIS.MyReporter.MobileClient.Config;
using FrankIS.MyReporter.MobileClient.Pages;

namespace FrankIS.MyReporter.MobileClient;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ConfigPage), typeof(ConfigPage));
    }
}
