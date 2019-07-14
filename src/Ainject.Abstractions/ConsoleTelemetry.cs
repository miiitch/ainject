using Ainject.Abstractions.Internals;

namespace Ainject.Abstractions
{
    public class ConsoleTelemetry : Telemetry
    {
        protected ConsoleTelemetry(TelemetryData data) : base(new ConsoleTelemetryClient(), data)
        {
        }

        protected ConsoleTelemetry() : base(new ConsoleTelemetryClient())
        {
        }
    }
}