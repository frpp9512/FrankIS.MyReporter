using FrankIS.ClockifyManagement.Configuration;
using FrankIS.ClockifyManagement.Contracts;
using FrankIS.ClockifyManagement.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FrankIS.ClockifyManagement.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddClockifyManager(this IServiceCollection services, Action<ClockifyConfiguration> configure)
    {
        _ = services.Configure(configure);
        _ = services.AddScoped<IClockifyManager, ClockifyManager>();

        return services;
    }
}
