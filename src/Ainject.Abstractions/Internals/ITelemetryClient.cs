using System;
using System.Collections.Generic;

namespace Ainject.Abstractions.Internals
{
    /// <summary>
    /// Internal use only
    /// </summary>
    public interface ITelemetryClient
    {
        void TrackTrace(string message, TraceSeverity severity,Dictionary<string, string> telemetryData);

        void TrackEvent(string eventName, Dictionary<string, string> telemetryData, Dictionary<string, double> metrics);

        void TrackMetric(string metricName, double value);

        void TrackMetric(string metricName, Dictionary<string, double> values);

        void TrackException(Exception exception, Dictionary<string, string> telemetryData,
            Dictionary<string, double> metrics);
    }
}