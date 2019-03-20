using System;
using System.Collections.Generic;
using Ainject.Abstractions;
using Ainject.Abstractions.Internals;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Ainject.ApplicationInsights.Internals
{

    internal sealed class ApplicationInsightTelemetryClient : ITelemetryClient
    {
        private readonly TelemetryClient _telemetryClient;

        public ApplicationInsightTelemetryClient(TelemetryClient telemetryClient)
        {
            if (telemetryClient is null) throw new ArgumentNullException(nameof(telemetryClient));

            _telemetryClient = telemetryClient;
        }


        private static SeverityLevel ConvertToSeverityLevel(TraceSeverity severity)
        {
            switch (severity)
            {
                case TraceSeverity.Verbose:
                    return SeverityLevel.Verbose;
                case TraceSeverity.Error:
                    return SeverityLevel.Error;
                case TraceSeverity.Warning:
                    return SeverityLevel.Warning;
                case TraceSeverity.Critical:
                    return SeverityLevel.Critical;
                case TraceSeverity.Information:
                    return SeverityLevel.Information;
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
            }
        }


        public void TrackTrace(string message, TraceSeverity severity,
            Dictionary<string, string> telemetryData)
        {           
            _telemetryClient.TrackTrace(message, ConvertToSeverityLevel(severity), telemetryData);  
                                       
        }

        public void TrackEvent(string eventName, Dictionary<string, string> telemetryData, Dictionary<string, double> metrics)
        {
            _telemetryClient.TrackEvent(eventName, telemetryData,metrics);
        }

        public void TrackMetric(string metricName, double value, Dictionary<string, string> telemetryData)
        {
            _telemetryClient.TrackMetric(metricName,value,telemetryData);
        }
    }
}