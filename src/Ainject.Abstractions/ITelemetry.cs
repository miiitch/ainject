using System;
using Ainject.Abstractions.Internals;

namespace Ainject.Abstractions
{
    public interface ITelemetry
    {
        void TrackTrace(string message, TraceSeverity severity, TelemetryData telemetryData = null);

        void TrackEvent(string eventName, TelemetryData telemetryData = null, TelemetryMetrics metrics = null);

        void TrackMetric(string metricName,double value);

        void TrackMetric(string metricName, string dimensionName, TelemetryMetrics values);
        
        void TrackException(Exception exception, TelemetryData telemetryData = null, TelemetryMetrics metrics = null);

        void TrackDependency(string dependencyTypeName, string dependencyName, string data,
            DateTimeOffset startTime, TimeSpan duration, bool success);
        
        ITelemetry CloneWith(TelemetryData telemetryData);
        
    }
}
