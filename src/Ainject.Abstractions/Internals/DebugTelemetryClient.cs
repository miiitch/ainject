using System.Diagnostics;

namespace Ainject.Abstractions.Internals
{
    public class DebugTelemetryClient : WritelineTelemetryClient
    {
        protected override void WriteLine(string line)
        {
            Debug.WriteLine(line);
        }
    }
}