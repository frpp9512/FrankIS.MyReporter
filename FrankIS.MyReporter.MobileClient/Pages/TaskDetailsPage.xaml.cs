using FrankIS.MyReporter.MobileClient.ViewModels;

namespace FrankIS.MyReporter.MobileClient.Pages;

public partial class TaskDetailsPage : ContentPage
{
	public TaskDetailsPage(TaskDetailsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}