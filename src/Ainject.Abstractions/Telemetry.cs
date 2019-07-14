using System;
using System.Collections.Generic;
using Ainject.Abstractions.Internals;

namespace Ainject.Abstractions
{
    public class Telemetry : ITelemetry
    {
        private readonly ITelemetryClient _client;
        private readonly TelemetryData _data;

        protected Telemetry(ITelemetryClient client, TelemetryData data)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            _client = client;
            _data = data;
        }

        protected Telemetry(ITelemetryClient client) : this(client, null)
        {

        }

        private void TrackTraceCore(string message, TraceSeverity severity,
            Dictionary<string, string> telemetryData)
        {
            _client.TrackTrace(message, severity, telemetryData);
        }

        private void TrackEventCore(string eventName,
            Dictionary<string, string> telemetryData,
            Dictionary<string, double> metrics)
        {
            _client.TrackEvent(eventName, telemetryData, metrics);
        }

        private void TrackMetricCore(string metricName, double value)
        {
            _client.TrackMetric(metricName, value);

        }

        private void TrackMetricCore(string metricName, Dictionary<string, double> values)
        {
            _client.TrackMetric(metricName, values);

        }



        private Dictionary<string, string> GenerateTelemetryDictionary(TelemetryData telemetryData)
        {
            Dictionary<string, string> result = null;
            if (_data == null || _data.IsEmpty)
            {
                result = telemetryData?.GetDictionary();
            }
            else if (telemetryData != null && !telemetryData.IsEmpty)
            {
                result = new Dictionary<string, string>();
                _data.CopyTo(result);
                telemetryData.CopyTo(result);
            }
            else
            {
                result = _data.GetDictionary();
            }

            return result;
        }

        public void TrackTrace(string message, TraceSeverity severity,
            TelemetryData telemetryData = null)
        {
            var traceData = GenerateTelemetryDictionary(telemetryData);

            TrackTraceCore(message, severity, traceData);
        }

        public void TrackEvent(string eventName, TelemetryData telemetryData = null, TelemetryMetrics metrics = null)
        {
            var eventData = GenerateTelemetryDictionary(telemetryData);

            TrackEventCore(eventName, eventData, metrics?.GetDictionary());
        }

        public void TrackMetric(string metricName, double value)
        {
            TrackMetricCore(metricName, value);
        }

        public void TrackMetric(string metricName, TelemetryMetrics values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));
            TrackMetricCore(metricName, values.GetDictionary());
        }

        public ITelemetry CloneWith(TelemetryData telemetryData)
        {
            var data = GenerateTelemetryDictionary(telemetryData);

            return new Telemetry(_client,new TelemetryData(data));
        }
    }
}