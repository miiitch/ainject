﻿using System.Collections.Generic;

namespace Ainject.Abstractions.Internals
{
    public abstract class WritelineTelemetryClient: ITelemetryClient
    {

        protected abstract void WriteLine(string line);

        private void Write(Dictionary<string, string> telemetryData)
        {
            if (telemetryData == null)
            {
                return;
            }
            foreach (var kvPair in telemetryData)
            {
                WriteLine($"> {kvPair.Key}={kvPair.Value}");
            }
        }

        private void Write(Dictionary<string, double> metrics)
        {
            if (metrics == null)
            {
                return;
            }
            foreach (var kvPair in metrics)
            {
                WriteLine($"> {kvPair.Key}={kvPair.Value:F}");
            }
        }

        public void TrackEvent(string eventName, Dictionary<string, string> telemetryData, Dictionary<string, double> metrics)
        {
            WriteLine($"[EVT] {eventName}");
            Write(telemetryData);
            Write(metrics);

        }

        public void TrackMetric(string metricName, double value)
        {
            WriteLine($"[MET] {metricName}={value:F}");
        }

        public void TrackMetric(string metricName, Dictionary<string, double> values)
        {
            WriteLine($"[MET] {metricName}");
            Write(values);
        }

        public void TrackTrace(string message, TraceSeverity severity, Dictionary<string, string> telemetryData)
        {
            WriteLine($"[TRC] ({severity}) {message}");
            Write(telemetryData);
        }
    }
}