using Ainject.Abstractions;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;

namespace Ainject.ApplicationInsights
{
    // ReSharper disable once InconsistentNaming
    public static class ServiceCollectionExtensions {

        public static IServiceCollection AddAInject(this IServiceCollection services)
        {
            return services.AddSingleton<ITelemetry, ApplicationInsightTelemetry>();
        }

        public static IServiceCollection AddAInject(this IServiceCollection services, TelemetryData defaultData)
        {
            return services.AddSingleton<ITelemetry>(svcs =>
            {
                var telemetryClient = svcs.GetService<TelemetryClient>();
                return new ApplicationInsightTelemetry(telemetryClient, defaultData);
            });
        }
    }
}