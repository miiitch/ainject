using Ainject.Abstractions;
using Microsoft.ApplicationInsights;

namespace Ainject.ApplicationInsights
{
    public class ApplicationInsightTelemetry : Telemetry
    {

        public ApplicationInsightTelemetry(TelemetryClient telemetryClient) : this(telemetryClient,null)
        {

        }

        public ApplicationInsightTelemetry(TelemetryClient telemetryClient, TelemetryData data) : base(
            new ApplicationInsightTelemetryClient(telemetryClient), data)
        {

        }
    }

}

   

