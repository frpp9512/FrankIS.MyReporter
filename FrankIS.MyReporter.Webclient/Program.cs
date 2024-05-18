using FrankIS.ClockifyManagement.Extensions;
using FrankIS.MyReporter.Management.Extensions;
using FrankIS.MyReporter.Management.Services;
using FrankIS.MyReporter.Webclient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTaskReporter(config =>
{
    IConfigurationSection reportSettingsSection = builder.Configuration.GetRequiredSection("ReportSettings");
    config.LunchStarts = reportSettingsSection.GetValue<TimeOnly>("LunchStarts");
    config.LunchDuration = reportSettingsSection.GetValue<int>("LunchDuration");
    config.WorkDayDuration = reportSettingsSection.GetValue<int>("WorkDayDuration");
    config.DayStartsAt = reportSettingsSection.GetValue<TimeOnly>("DayStartsAt");
    config.WorkspaceId = reportSettingsSection.GetValue<string>("WorkspaceId") ?? "";
})
    .AddTaskReportGenerator<DefaultNewTaskReportGenerator>();

builder.Services.AddOpenProjectManager(config =>
{
    config.ApiKey = builder.Configuration["OpenProject:ApiKey"] ?? throw new ApplicationException("Must provide an OpenProject api key");
    config.BaseUrl = builder.Configuration["OpenProject:BaseUrl"] ?? "https://project.webavanx.com/api/v3/"; 
});

builder.Services.AddClockifyManager(config =>
{
    config.ApiKey = builder.Configuration["Clockify:ApiKey"] ?? throw new ApplicationException("Must provide a Clockify api key");
    config.ClockifyApiBaseUrl = builder.Configuration["Clockify:BaseUrl"] ?? "https://api.clockify.me/api/v1/";
});

builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();
