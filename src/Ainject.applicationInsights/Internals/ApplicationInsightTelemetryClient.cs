﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ainject.Abstractions;
using Ainject.Abstractions.Internals;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Metrics;

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
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));

            _telemetryClient.TrackTrace(message, ConvertToSeverityLevel(severity), telemetryData);
        }

        public void TrackEvent(string eventName, Dictionary<string, string> telemetryData, Dictionary<string, double> metrics)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(eventName));

            _telemetryClient.TrackEvent(eventName, telemetryData,metrics);
        }

        public void TrackMetric(string metricName, double value)
        {
            if (string.IsNullOrWhiteSpace(metricName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(metricName));

            _telemetryClient.GetMetric(metricName).TrackValue(value);
        }

        

        public void TrackMetric(string metricName, string dimensionName, Dictionary<string, double> values)
        {
         
            if (values is null) throw new ArgumentNullException(nameof(values));
            if (string.IsNullOrWhiteSpace(metricName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(metricName));

            var metricDefinition = new MetricIdentifier(MetricIdentifier.DefaultMetricNamespace, 
                metricName,
                dimensionName);
            
            var metric = _telemetryClient.GetMetric(metricDefinition);
            
            foreach (var kvp in values)
            {
                metric.TrackValue(kvp.Value,kvp.Key);
            }
        }


        public void TrackException(Exception exception, Dictionary<string, string> telemetryData, Dictionary<string, double> metrics)
        {
            _telemetryClient.TrackException(exception, telemetryData, metrics);
        }


        public void TrackDependency(string dependencyTypeName, string dependencyName, string data,DateTimeOffset startTime,TimeSpan duration, bool success )
        {
            _telemetryClient.TrackDependency(dependencyTypeName,dependencyName,data, startTime, duration, success);
        }
    }
}