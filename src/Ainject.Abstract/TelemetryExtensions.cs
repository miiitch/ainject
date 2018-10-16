namespace Ainject.Abstract
{
    public static class TelemetryExtensions
    {
        public static TelemetryData MergeData(this TelemetryData[] info, bool alwaysClone = false)
        {
            TelemetryData telemetryInfo;
            if (info.Length == 1 && !alwaysClone)
            {            
                telemetryInfo = info[0];
            }
            else
            {
                telemetryInfo = new TelemetryData();
                telemetryInfo.Append<TelemetryData, string>(info);
            }

            return telemetryInfo;
        }


        public static void TrackVerbose(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = MergeData(info);

            telemetry.TrackTrace(message, TraceSeverity.Verbose, telemetryInfo);
        }

        public static void TrackInformation(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = MergeData(info);

            telemetry.TrackTrace(message, TraceSeverity.Information, telemetryInfo);
        }

        public static void TrackCritical(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = MergeData(info);

            telemetry.TrackTrace(message, TraceSeverity.Critical, telemetryInfo);
        }

        public static void TrackWarning(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = MergeData(info);

            telemetry.TrackTrace(message, TraceSeverity.Warning, telemetryInfo);
        }

        public static void TrackError(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            var telemetryInfo = MergeData(info);

            telemetry.TrackTrace(message, TraceSeverity.Error, telemetryInfo);
        }

        public static void Append<TTelemetry,TData>(this TTelemetry telemetry,params TTelemetry[] infoArray) where TTelemetry: TelemetryInfo<TData>
        {
            if (infoArray == null)
            {
                return;
            }

            foreach (var info in infoArray)
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