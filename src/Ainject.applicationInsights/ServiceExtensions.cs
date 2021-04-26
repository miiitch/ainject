using Ainject.Abstractions;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;

namespace Ainject.ApplicationInsights
{
    // ReSharper disable once InconsistentNaming
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures AINject with Application Insights
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAInject(this IServiceCollection services)
        {
            return services.AddSingleton<ITelemetry, ApplicationInsightTelemetry>();
        }
        
        /// <summary>
        /// Configures AInject with Application Insights and a custom telemetry data
        /// </summary>
        /// <param name="services"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
        public static IServiceCollection AddAInject(this IServiceCollection services, TelemetryData defaultData)
        {
            
            return services.AddSingleton<ITelemetry>(svcs =>
            {
                var telemetryClient = svcs.GetService<TelemetryClient>();

                return new ApplicationInsightTelemetry(telemetryClient, defaultData);
            });
        }
        
        /// <summary>
        /// Configures AInject with a custom telemetry
        /// </summary>
        /// <param name="services"></param>
        /// <param name="telemetry"></param>
        /// <returns></returns>
        public static IServiceCollection AddAInject(this IServiceCollection services, ITelemetry telemetry)
        {
            return services.AddSingleton<ITelemetry>(telemetry);
        }
    }
}
