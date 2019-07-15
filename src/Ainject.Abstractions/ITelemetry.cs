using System;

namespace Ainject.Abstractions
{
    public interface ITelemetry
    {
        void TrackTrace(string message, TraceSeverity severity, TelemetryData telemetryData = null);

        void TrackEvent(string eventName, TelemetryData telemetryData = null, TelemetryMetrics metrics = null);

        void TrackMetric(string metricName,double value);

        void TrackMetric(string metricName, TelemetryMetrics values);

        void TrackException(Exception exception, TelemetryData telemetryData = null, TelemetryMetrics metrics = null);

        ITelemetry CloneWith(TelemetryData telemetryData);
    }
}
