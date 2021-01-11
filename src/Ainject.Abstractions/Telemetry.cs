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

        private void TrackMetricCore(string metricName,string dimensionName, Dictionary<string, double> values)
        {
            _client.TrackMetric(metricName,dimensionName, values);
        }

        private void TrackExceptionCore(Exception exception, Dictionary<string, string> telemetryData,
            Dictionary<string, double> metrics)
        {
            _client.TrackException(exception, telemetryData, metrics);
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

        public void TrackMetric(string metricName, string dimensionName, TelemetryMetrics values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));
            TrackMetricCore(metricName, dimensionName,values.GetDictionary());
        }

        public void TrackException(Exception exception, TelemetryData telemetryData = null, TelemetryMetrics metrics = null)
        {
            if (exception is null) throw new ArgumentNullException(nameof(exception));

            var eventData = GenerateTelemetryDictionary(telemetryData);

            TrackExceptionCore(exception, eventData, metrics?.GetDictionary());
        }

        public void TrackDependency(string dependencyTypeName, string dependencyName, string data,
            DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            _client.TrackDependency(dependencyTypeName, dependencyName, data, startTime, duration, success);
        }


        public ITelemetry CloneWith(TelemetryData telemetryData)
        {
            var data = GenerateTelemetryDictionary(telemetryData);

            return new Telemetry(_client, new TelemetryData(data));
        }
        
        
    }
}