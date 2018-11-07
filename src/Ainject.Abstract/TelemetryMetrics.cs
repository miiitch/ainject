namespace Ainject.Abstract
{
    public sealed class TelemetryMetrics : TelemetryInfo<double>
    {
        public TelemetryMetrics(TelemetryMetrics metrics = null) : base(metrics?.Dictionary)
        {
        }
    }
}