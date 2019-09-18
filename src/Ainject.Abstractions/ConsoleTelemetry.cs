using Ainject.Abstractions.Internals;

namespace Ainject.Abstractions
{
    public class ConsoleTelemetry : Telemetry
    {
     
        public ConsoleTelemetry(TelemetryData data) : base(new ConsoleTelemetryClient(), data)
        {
        }

        public ConsoleTelemetry() : base(new ConsoleTelemetryClient())
        {
        }
    }
}