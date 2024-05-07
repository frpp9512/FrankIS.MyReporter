using FrankIS.ClockifyManagement.Extensions;
using FrankIS.MyReporter.Management.Extensions;
using FrankIS.MyReporter.Webclient;
using FrankIS.MyReporter.Webclient.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTaskReporter(config =>
{
    var reportSettingsSection = builder.Configuration.GetRequiredSection("ReportSettings");
    config.LunchStarts = reportSettingsSection.GetValue<TimeOnly>("LunchStarts");
    config.LunchDuration = reportSettingsSection.GetValue<int>("LunchDuration");
    config.WorkDayDuration = reportSettingsSection.GetValue<int>("WorkDayDuration");
    config.DayStartsAt = reportSettingsSection.GetValue<TimeOnly>("DayStartsAt");
    config.WorkspaceId = reportSettingsSection.GetValue<string>("WorkspaceId") ?? "";
});

builder.Services.AddClockifyManager(config =>
{
    config.ApiKey = builder.Configuration["Clockify:ApiKey"] ?? throw new ApplicationException("Must provide a Clockify api key");
    config.ClockifyApiBaseUrl = builder.Configuration["Clockify:BaseUrl"] ?? "https://api.clockify.me/api/v1/";
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
