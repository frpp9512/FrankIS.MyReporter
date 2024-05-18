using FrankIS.MyReporter.MobileClient.ViewModels;

namespace FrankIS.MyReporter.MobileClient.Pages;

public partial class TasksPage : ContentPage
{
	public TasksPage(TasksPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}