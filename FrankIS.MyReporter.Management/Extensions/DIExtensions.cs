using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Management.Services;
using FrankIS.MyReporter.Webclient.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrankIS.MyReporter.Management.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddTaskReporter(this IServiceCollection services, Action<ReportSettings> configure)
    {
        _ = services.Configure(configure);
        _ = services.AddScoped<ITaskReporter, TaskReporter>();
        _ = services.AddTransient<ITaskReportGenerator, TaskReportGenerator>();

        return services;
    }
}
