using FrankIS.MyReporter.Management.Configuration;
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

        return services;
    }

    public static IServiceCollection AddTaskReportGenerator<TGenerator>(this IServiceCollection services) where TGenerator : INewTaskReportGenerator
    {
        services.AddTransient(typeof(INewTaskReportGenerator), typeof(TGenerator));
        return services;
    }

    public static IServiceCollection AddOpenProjectManager(this IServiceCollection services, Action<OpenProjectSettings> configure)
    {
        _ = services.Configure(configure);
        _ = services.AddScoped<IOpenProjectManager, OpenProjectManager>();

        return services;
    }
}
