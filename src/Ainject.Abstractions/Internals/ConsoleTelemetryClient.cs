using System;

namespace Ainject.Abstractions.Internals
{
    internal class ConsoleTelemetryClient : WritelineTelemetryClient
    {
        protected override void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}