using System;
using System.Collections.Generic;

namespace Ainject.Abstractions
{
    public static class TelemetryExtensions
    {
        public static TelemetryData Merge(this TelemetryData[] info, bool alwaysClone = false)
        {
            TelemetryData telemetryInfo;
            if (info.Length == 1 && !alwaysClone)
            {            
                telemetryInfo = info[0];
            }
            else
            {
                telemetryInfo = new TelemetryData();
                telemetryInfo.AppendAll(info);
            }

            return telemetryInfo;
        }


        public static void TrackVerbose(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = Merge(info);

            telemetry.TrackTrace(message, TraceSeverity.Verbose, telemetryInfo);
        }

        public static void TrackInformation(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = Merge(info);

            telemetry.TrackTrace(message, TraceSeverity.Information, telemetryInfo);
        }

        public static void TrackCritical(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = Merge(info);

            telemetry.TrackTrace(message, TraceSeverity.Critical, telemetryInfo);
        }

        public static void TrackWarning(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = Merge(info);

            telemetry.TrackTrace(message, TraceSeverity.Warning, telemetryInfo);
        }

        public static void TrackError(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = Merge(info);

            telemetry.TrackTrace(message, TraceSeverity.Error, telemetryInfo);
        }

        public static void AppendAll(this TelemetryData telemetry, IEnumerable<TelemetryData> telemetries)
        {
            if (telemetries is null) throw new ArgumentNullException(nameof(telemetries));

            foreach (var info in telemetries)
            {
                if (info == null)
                {
                    continue;
                    
                }
                foreach (var kv in info.Dictionary)
                {
                    telemetry.Dictionary[kv.Key] = kv.Value;
                }
              
            }
        }

        
    }
}