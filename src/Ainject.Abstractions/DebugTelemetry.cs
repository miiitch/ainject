using Ainject.Abstractions.Internals;

namespace Ainject.Abstractions
{
    public class DebugTelemetry: Telemetry
    {
        public DebugTelemetry(TelemetryData data) : base(new DebugTelemetryClient(), data)
        {
        }

        public DebugTelemetry() : base(new DebugTelemetryClient())
        {
        }
    }
}