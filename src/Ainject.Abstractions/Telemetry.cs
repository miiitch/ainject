using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ainject.Abstractions.Internals;

[assembly: InternalsVisibleTo("Ainject.UnitTests")]

namespace Ainject.Abstractions
{
    public class Telemetry : ITelemetry
    {
        private readonly ITelemetryClient _client;
        private readonly TelemetryData _data;

        protected internal Telemetry(ITelemetryClient client, TelemetryData data)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));

            _client = client;
            _data = data;
        }

        protected internal Telemetry(ITelemetryClient client) : this(client, null)
        {
        }

        private void TrackTraceCore(string message, TraceSeverity severity,
            Dictionary<string, string> data)
        {
            _client.TrackTrace(message, severity, data);
        }

        private void TrackEventCore(string eventName,
            Dictionary<string, string> data,
            Dictionary<string, double> metrics)
        {
            _client.TrackEvent(eventName, data, metrics);
        }

        private void TrackMetricCore(string metricName, double value)
        {
            _client.TrackMetric(metricName, value);
        }

        private void TrackMetricCore(string metricName, string dimensionName, Dictionary<string, double> values)
        {
            _client.TrackMetric(metricName, dimensionName, values);
        }

        private void TrackExceptionCore(Exception exception, Dictionary<string, string> data,
            Dictionary<string, double> metrics)
        {
            _client.TrackException(exception, data, metrics);
        }


        private Dictionary<string, string> GenerateTelemetryDictionary(TelemetryData extraTelemetryData)
        {
            if (_data is null or {IsEmpty: true})
            {
                return extraTelemetryData?.GetDictionary();
            }

            if (extraTelemetryData is null or {IsEmpty: true})
            {
                return _data.GetDictionary();
            }

            var result = new Dictionary<string, string>();
            _data.CopyTo(result);
            extraTelemetryData.CopyTo(result);

            return result;
        }

        public void TrackTrace(string message, TraceSeverity severity,
            TelemetryData data = null)
        {
            var traceData = GenerateTelemetryDictionary(data);

            TrackTraceCore(message, severity, traceData);
        }

        public void TrackEvent(string eventName, TelemetryData data = null, TelemetryMetrics metrics = null)
        {
            var eventData = GenerateTelemetryDictionary(data);

            TrackEventCore(eventName, eventData, metrics?.GetDictionary());
        }

        public void TrackMetric(string metricName, double value)
        {
            TrackMetricCore(metricName, value);
        }

        public void TrackMetric(string metricName, string dimensionName, TelemetryMetrics values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

            TrackMetricCore(metricName, dimensionName, values.GetDictionary());
        }

        public void TrackException(Exception exception, TelemetryData data = null, TelemetryMetrics metrics = null)
        {
            if (exception is null) throw new ArgumentNullException(nameof(exception));

            var eventData = GenerateTelemetryDictionary(data);

            TrackExceptionCore(exception, eventData, metrics?.GetDictionary());
        }

        public void TrackDependency(string dependencyTypeName, string dependencyName, string data,
            DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            _client.TrackDependency(dependencyTypeName, dependencyName, data, startTime, duration, success);
        }


        public ITelemetry CloneWith(TelemetryData data)
        {
            var clonedData = GenerateTelemetryDictionary(data);

            return new Telemetry(_client, new TelemetryData(clonedData));
        }
    }
}
