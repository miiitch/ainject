using Ainject.Abstractions.Internals;

namespace Ainject.Abstractions
{
    public class DebugTelemetry: Telemetry
    {
        protected DebugTelemetry(TelemetryData data) : base(new DebugTelemetryClient(), data)
        {
        }

        protected DebugTelemetry() : base(new DebugTelemetryClient())
        {
        }
    }
}