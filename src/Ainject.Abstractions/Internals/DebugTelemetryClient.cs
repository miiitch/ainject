using System.Diagnostics;

namespace Ainject.Abstractions.Internals
{
    internal class DebugTelemetryClient : WritelineTelemetryClient
    {
        protected override void WriteLine(string line)
        {
            Debug.WriteLine(line);
        }
    }
}