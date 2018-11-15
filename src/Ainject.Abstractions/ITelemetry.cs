namespace Ainject.Abstractions
{
    public interface ITelemetry
    {
        void TrackTrace(string message, TraceSeverity severity, TelemetryData telemetryData = null);

        void TrackEvent(string eventName, TelemetryData telemetryData = null, TelemetryMetrics metrics = null);

        void TrackMetric(string metricName,double value, TelemetryData telemetryData = null);

        ITelemetry CloneWith(TelemetryData telemetryData);
    }
}
