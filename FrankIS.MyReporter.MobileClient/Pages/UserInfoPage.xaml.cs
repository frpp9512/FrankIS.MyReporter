using FrankIS.MyReporter.MobileClient.ViewModels;

namespace FrankIS.MyReporter.MobileClient.Pages;

public partial class UserInfoPage : ContentPage
{
	public UserInfoPage(UserInfoPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}