using System;
using System.Diagnostics;

namespace Ainject.Abstractions
{
    public class StopwatchBlock : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        public  DateTimeOffset StartDate { get; }
        public TimeSpan Elapsed => _stopwatch.Elapsed;
        
        public StopwatchBlock()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            StartDate = DateTimeOffset.UtcNow;
        }

        public void Stop()
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();    
            }
            
        }

        public void Dispose()
        {
            Stop();
        }

        public TelemetryMetrics ToTelemetryMetrics()
        {
            return _stopwatch.ToTelemetryMetrics();
        }

        
    }
}