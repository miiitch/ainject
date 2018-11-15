using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Ainject.Abstractions;
using NFluent;
using NSubstitute;
using Xunit;

namespace Ainject.UnitTests
{
   
   [ExcludeFromCodeCoverage]
    public class TelemetryShould
    {
       
        private readonly ITelemetryClient _client;
        

        public TelemetryShould()
        {
            _client = Substitute.For<ITelemetryClient>();
            
        }

        [Fact]
        public void Call_TraceTrace_with_Information_level_when_TrackInformation_is_called()
        {
            var telemetry = new Telemetry(_client);

            var message = "TraceMessage";

            telemetry.TrackInformation(message);


            _client.Received().TrackTrace(message, TraceSeverity.Information, Arg.Is<Dictionary<string, string>>(data => data == null || data.Count == 0));
        }

        [Fact]
        public void Call_TraceTrace_with_Critical_level_when_TrackCritical_is_called()
        {
            var telemetry = new Telemetry(_client);

            var message = "TraceMessage";

            telemetry.TrackCritical(message);


            _client.Received().TrackTrace(message, TraceSeverity.Critical, Arg.Is<Dictionary<string, string>>(data => data == null || data.Count == 0));
        }

        [Fact]
        public void Call_TraceTrace_with_Error_level_when_TrackError_is_called()
        {
            var telemetry = new Telemetry(_client);

            var message = "TraceMessage";

            telemetry.TrackError(message);


            _client.Received().TrackTrace(message, TraceSeverity.Error, Arg.Is<Dictionary<string, string>>(data => data == null || data.Count == 0));
        }

        [Fact]
        public void Call_TraceTrace_with_Verbose_level_when_TrackVerbose_is_called()
        {
            var telemetry = new Telemetry(_client);

            var message = "TraceMessage";

            telemetry.TrackVerbose(message);


            _client.Received().TrackTrace(message, TraceSeverity.Verbose, Arg.Is<Dictionary<string, string>>(data => data == null || data.Count == 0));
        }

        [Fact]
        public void Call_TraceTrace_with_Warning_level_when_TrackWarning_is_called()
        {
            var telemetry = new Telemetry(_client);

            var message = "TraceMessage";

            telemetry.TrackWarning(message);


            _client.Received().TrackTrace(message, TraceSeverity.Warning, Arg.Is<Dictionary<string, string>>(data => data == null || data.Count == 0));
        }



        [Theory]
        [InlineData(TraceSeverity.Critical)]
        [InlineData(TraceSeverity.Error)]
        [InlineData(TraceSeverity.Information)]
        [InlineData(TraceSeverity.Verbose)]
        [InlineData(TraceSeverity.Warning)]
        public void Call_TrackTrace(TraceSeverity severity)
        {
            var telemetry = new Telemetry(_client);

            var message = "TraceMessage";

            telemetry.TrackTrace(message, severity, null);


            _client.Received().TrackTrace(message, severity, Arg.Is<Dictionary<string,string>>(data => data == null || data.Count == 0 ));
        }

        [Theory]
        [InlineData(TraceSeverity.Critical)]
        [InlineData(TraceSeverity.Error)]
        [InlineData(TraceSeverity.Information)]
        [InlineData(TraceSeverity.Verbose)]
        [InlineData(TraceSeverity.Warning)]
        public void Call_TrackTrace_WithDataInTrace(TraceSeverity severity)
        {
            var telemetry = new Telemetry(_client);

            var message = "TraceMessage";
            var telemetryDataTraceKey = "A";
            var telemetryDataTraceValue = "B";
            var telemetryData = new TelemetryData {[telemetryDataTraceKey] = telemetryDataTraceValue };


            telemetry.TrackTrace(message, severity, telemetryData);


            _client.Received().TrackTrace(message, severity, Arg.Is<Dictionary<string, string>>(data => data != null && data.Count == 1 && data[telemetryDataTraceKey] == telemetryDataTraceValue));

        }

        [Theory]
        [InlineData(TraceSeverity.Critical)]
        [InlineData(TraceSeverity.Error)]
        [InlineData(TraceSeverity.Information)]
        [InlineData(TraceSeverity.Verbose)]
        [InlineData(TraceSeverity.Warning)]
        public void Call_TrackTrace_WithDataInTraceAndTelemetry(TraceSeverity severity)
        {
            const string telemetryDataKey = "B";
            const string telemetryDataValue = "C";
            var telemetryData =new TelemetryData { [telemetryDataKey] = telemetryDataValue };

            var telemetry = new Telemetry(_client, telemetryData);

            const string message = "TraceMessage";
            const string telemetryDataTraceKey = "A";
            const string telemetryDataTraceValue = "B";
            telemetryData = new TelemetryData { [telemetryDataTraceKey] = telemetryDataTraceValue };


            telemetry.TrackTrace(message, severity, telemetryData);


            _client.Received().TrackTrace(message, severity, Arg.Is<Dictionary<string, string>>(data => 
                data != null && 
                data.Count == 2 &&
                data[telemetryDataKey] == telemetryDataValue && 
                data[telemetryDataTraceKey] == telemetryDataTraceValue));

        }

        [Theory]
        [InlineData(TraceSeverity.Critical)]
        [InlineData(TraceSeverity.Error)]
        [InlineData(TraceSeverity.Information)]
        [InlineData(TraceSeverity.Verbose)]
        [InlineData(TraceSeverity.Warning)]
        public void Call_TrackTrace_WithDataInTraceAndTelemetry_WithOverride(TraceSeverity severity)
        {
            const string telemetryDataKey = "A";
            const string telemetryDataValue = "C";
            var telemetryData = new TelemetryData { [telemetryDataKey] = telemetryDataValue };

            var telemetry = new Telemetry(_client, telemetryData);

            const string message = "TraceMessage";
            const string telemetryDataTraceKey = "A";
            const string telemetryDataTraceValue = "B";
            telemetryData = new TelemetryData { [telemetryDataTraceKey] = telemetryDataTraceValue };


            telemetry.TrackTrace(message, severity, telemetryData);


            _client.Received().TrackTrace(message, severity, Arg.Is<Dictionary<string, string>>(data =>
                data != null &&
                data.Count == 1 &&                
                data[telemetryDataTraceKey] == telemetryDataTraceValue));

        }

        [Theory]
        [InlineData(TraceSeverity.Critical)]
        [InlineData(TraceSeverity.Error)]
        [InlineData(TraceSeverity.Information)]
        [InlineData(TraceSeverity.Verbose)]
        [InlineData(TraceSeverity.Warning)]
        public void Call_TrackTrace_WithDataInTelemetry(TraceSeverity severity)
        {
            const string telemetryDataKey = "B";
            const string telemetryDataValue = "C";
            var telemetryData = new TelemetryData { [telemetryDataKey] = telemetryDataValue };

            var telemetry = new Telemetry(_client, telemetryData);

            const string message = "TraceMessage";
          
            telemetry.TrackTrace(message, severity);

            _client.Received().TrackTrace(message, severity, Arg.Is<Dictionary<string, string>>(data =>
                data != null &&
                data.Count == 1 &&
                data[telemetryDataKey] == telemetryDataValue));

        }

        [Fact]
        public void Call_TrackEvent()
        {
            var telemetry = new Telemetry(_client);

            var eventName = "EventName";

            telemetry.TrackEvent(eventName);


            _client.Received().TrackEvent(eventName, 
                Arg.Is<Dictionary<string, string>>(data => data == null || data.Count == 0),
                Arg.Is<Dictionary<string, double>>(metrics => metrics == null || metrics.Count == 0)
                );
        }

        [Fact]
        public void Call_TrackEvent_WithDataInEvent()
        {
            var telemetry = new Telemetry(_client);

            const string eventName = "EventName";
            
            const string telemetryDataEventKey = "A";
            const string telemetryDataEventValue = "B";
            var telemetryData = new TelemetryData { [telemetryDataEventKey] = telemetryDataEventValue };
            telemetry.TrackEvent(eventName, telemetryData);


            _client.Received().TrackEvent(eventName,
                Arg.Is<Dictionary<string, string>>(data =>
                        data != null &&
                        data.Count == 1 &&
                        data[telemetryDataEventKey] == telemetryDataEventValue),
                Arg.Is<Dictionary<string, double>>(metrics => metrics == null || metrics.Count == 0)
            );
        }

        [Fact]
        public void Call_TrackEvent_WithMetricInEvent()
        {
            var telemetry = new Telemetry(_client);

            const string eventName = "EventName";

            const string telemetryMetricEventKey = "A";
            const double telemetryMetricEventValue = 5;
            var telemetryMetrics = new TelemetryMetrics() { [telemetryMetricEventKey] = telemetryMetricEventValue };
            telemetry.TrackEvent(eventName, null, telemetryMetrics);

            _client.Received().TrackEvent(eventName,
                Arg.Is<Dictionary<string, string>>(data =>
                    data == null ||
                    data.Count == 0),
                Arg.Is<Dictionary<string, double>>(metrics => 
                    metrics != null && 
                    metrics.Count == 1 &&
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    metrics[telemetryMetricEventKey] == telemetryMetricEventValue)
            );
        }

        [Fact]
        public void Call_TrackMetric()
        {
            var telemetry = new Telemetry(_client);

            const string metricName = "MetricName";
            const double metricValue = 4.3;

            telemetry.TrackMetric(metricName, metricValue);

            _client.Received().TrackMetric(metricName, metricValue,
                Arg.Is<Dictionary<string, string>>(data => data == null || data.Count == 0));
        }
         [Fact]
        public void Call_TrackMetric_WithData()
        {
            var telemetry = new Telemetry(_client);

            const string metricName = "MetricName";
            const double metricValue = 4.3;

            const string telemetryDataEventKey = "A";
            const string telemetryDataEventValue = "B";
            var telemetryData = new TelemetryData { [telemetryDataEventKey] = telemetryDataEventValue };

            telemetry.TrackMetric(metricName, metricValue, telemetryData);


            _client.Received().TrackMetric(metricName, metricValue,
                Arg.Is<Dictionary<string, string>>(data =>
                    data != null &&
                    data.Count == 1 &&
                    data[telemetryDataEventKey] == telemetryDataEventValue));

        }

        [Fact]
        public void Throw_Exception_IfClientIsNull()
        {
            Check.ThatCode(() => new Telemetry(null)).Throws<ArgumentNullException>();
        }

        [Fact]
        public void Call_TrackTrace_WithDataInTraceAndTelemetry_AndTelemetryCreatedWithData()
        {
            const string telemetryDataKey = "B";
            const string telemetryDataValue = "C";
            const TraceSeverity severity = TraceSeverity.Information;
            var telemetryData = new TelemetryData { [telemetryDataKey] = telemetryDataValue };

            var telemetry = new Telemetry(_client, telemetryData);

            const string addedTelemetryKey = "C";
            const string addedTelemetryValue = "B";
            var addedTelemetryData  = new TelemetryData { [addedTelemetryKey] = addedTelemetryValue };

            var createdTelemetry = telemetry.CloneWith(addedTelemetryData);

            const string message = "TraceMessage";
            const string telemetryDataTraceKey = "A";
            const string telemetryDataTraceValue = "B";
            telemetryData = new TelemetryData { [telemetryDataTraceKey] = telemetryDataTraceValue };

            createdTelemetry.TrackTrace(message, severity, telemetryData);

            _client.Received().TrackTrace(message, severity, Arg.Is<Dictionary<string, string>>(data =>
                data != null &&
                data.Count == 3 &&
                data[telemetryDataKey] == telemetryDataValue &&            
                data[addedTelemetryKey] == addedTelemetryValue &&
                data[telemetryDataTraceKey] == telemetryDataTraceValue));
        }

    }
}
