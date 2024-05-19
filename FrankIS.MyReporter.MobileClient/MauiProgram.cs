using CommunityToolkit.Maui;
using FrankIS.ClockifyManagement.Extensions;
using FrankIS.MyReporter.Management.Extensions;
using FrankIS.MyReporter.Management.Services;
using FrankIS.MyReporter.MobileClient.Config;
using FrankIS.MyReporter.MobileClient.Data;
using FrankIS.MyReporter.MobileClient.Pages;
using FrankIS.MyReporter.MobileClient.ViewModels;
using Microsoft.Extensions.Logging;

namespace FrankIS.MyReporter.MobileClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<TaskDetailsPageViewModel>();
        builder.Services.AddTransient<TaskDetailsPage>();

        builder.Services.AddSingleton<UserInfoPageViewModel>();
        builder.Services.AddSingleton<UserInfoPage>();

        builder.Services.AddTransient<CreateOrEditPageViewModel>();
        builder.Services.AddTransient<CreateOrEditTaskPage>();

        builder.Services.AddSingleton<TasksPageViewModel>();
        builder.Services.AddSingleton<TasksPage>();

        builder.Services.AddSingleton<ConfigPageViewModel>();
        builder.Services.AddSingleton<ConfigPage>();

        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

        ApplicationConfig.IsConfigured = Preferences.Get("IsConfigured", false);

        builder.Services.AddClockifyManager(config =>
        {
            config.ApiKey = Preferences.Get("Clockify:ApiKey", "<not_set>");
            config.ClockifyApiBaseUrl = Preferences.Get("Clockify:BaseUrl", "https://api.clockify.me/api/v1/");
        });

        builder.Services.AddTaskReporter(config =>
        {
            config.LunchStarts = TimeOnly.FromDateTime(Preferences.Get("LunchStarts", new DateTime(DateOnly.MinValue, new TimeOnly(12, 00))));
            config.LunchDuration = Preferences.Get("LunchDuration", 1);
            config.WorkDayDuration = Preferences.Get("WorkDayDuration", 8);
            config.DayStartsAt = TimeOnly.FromDateTime(Preferences.Get("DayStartsAt", new DateTime(DateOnly.MinValue, new TimeOnly(12, 00))));
            config.WorkspaceId = Preferences.Get("WorkspaceId", "<not set>");
        })
            .AddTaskReportGenerator<DefaultNewTaskReportGenerator>();

        builder.Services.AddOpenProjectManager(config =>
        {
            config.ApiKey = Preferences.Get("OpenProject:ApiKey", "b05b8120ada58b30cbd6ceab191b1482141454f6323c6ebcf5daa8700569c1bc");
            config.BaseUrl = Preferences.Get("OpenProject:BaseUrl", "https://project.webavanx.com/api/v3/");
        });

        builder.Services.AddSingleton<ReportTasksRepository>();

        return builder.Build();
    }
}
