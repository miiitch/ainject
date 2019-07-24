using System;
using System.Diagnostics;

namespace Ainject.Abstractions
{
    public class StopwatchBlock : IDisposable
    {
        private readonly Stopwatch _stopwatch;

        public StopwatchBlock()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }


        public void Dispose()
        {
            _stopwatch.Stop();
        }

        public TelemetryMetrics ToTelemetryMetrics()
        {
            return _stopwatch.ToTelemetryMetrics();
        }

        public TimeSpan Elapsed => _stopwatch.Elapsed;
    }
}