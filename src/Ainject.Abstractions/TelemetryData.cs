using System.Collections.Generic;

namespace Ainject.Abstractions
{
    public sealed class TelemetryData: TelemetryInfo<string>
    {
        public TelemetryData(TelemetryData data = null) : base(data?.Dictionary)
        {            
        }

        internal TelemetryData(Dictionary<string, string> values): base(values)
        {

        }
    }
}