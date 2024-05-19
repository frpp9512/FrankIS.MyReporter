using FrankIS.MyReporter.MobileClient.ViewModels;

namespace FrankIS.MyReporter.MobileClient.Pages;

public partial class CreateOrEditTaskPage : ContentPage
{
    public CreateOrEditTaskPage(CreateOrEditPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}