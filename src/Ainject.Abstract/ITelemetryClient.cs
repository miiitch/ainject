﻿using System.Collections.Generic;

namespace Ainject.Abstract
{
    public interface ITelemetryClient
    {
        void TrackTrace(string message, TraceSeverity severity,Dictionary<string, string> telemetryData);

        void TrackEvent(string eventName, Dictionary<string, string> telemetryData, Dictionary<string, double> metrics);

        void TrackMetric(string metricName, double value, Dictionary<string, string> telemetryData);
    }
}