using FrankIS.MyReporter.MobileClient.Pages;

namespace FrankIS.MyReporter.MobileClient;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ConfigPage), typeof(ConfigPage));
        Routing.RegisterRoute(nameof(CreateOrEditTaskPage), typeof(CreateOrEditTaskPage));
        Routing.RegisterRoute(nameof(TaskDetailsPage), typeof(TaskDetailsPage));
        Routing.RegisterRoute(nameof(NoNetworkPage), typeof(NoNetworkPage));
    }
}
