using Ainject.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Ainject.ApplicationInsights
{
    // ReSharper disable once InconsistentNaming
    public static class ServiceCollectionExtensions {

        public static IServiceCollection AddAInject(this IServiceCollection services)
        {
            return services.AddScoped<ITelemetry, ApplicationInsightTelemetry>();
        }
    }
}