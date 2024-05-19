namespace FrankIS.MyReporter.MobileClient.Pages;

public partial class NoNetworkPage : ContentPage
{
	public NoNetworkPage()
	{
		InitializeComponent();
	}

    private async void Retry(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

    private async void GoToTasks(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(TasksPage)}");
    }
}
