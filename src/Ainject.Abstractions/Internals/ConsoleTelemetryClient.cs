using System;

namespace Ainject.Abstractions.Internals
{
    public class ConsoleTelemetryClient : WritelineTelemetryClient
    {
        protected override void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}