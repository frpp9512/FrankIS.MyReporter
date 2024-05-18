using FrankIS.MyReporter.MobileClient.ViewModels;

namespace FrankIS.MyReporter.MobileClient.Pages;

public partial class ConfigPage : ContentPage
{
    public ConfigPage(ConfigPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
