using System;
using System.Collections.Generic;

namespace Ainject.Abstract
{
    public sealed class TelemetryData: TelemetryInfo<string>
    {

        public TelemetryData()
        {

        }

        public TelemetryData(TelemetryData data) : this()
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            data.CopyTo(Dictionary);
        }

        public TelemetryData(Dictionary<string,string> data) : base()
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            Append(data);            
        }


    }
}