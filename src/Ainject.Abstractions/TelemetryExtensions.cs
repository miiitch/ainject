using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ainject.Abstractions.Internals;

namespace Ainject.Abstractions
{
    public static class TelemetryExtensions
    {
        public static TelemetryData Merge(this TelemetryData[] info)
        {
            var result = new TelemetryData();
            result.AppendAll(info);
            
            return result;
        }

        private static TelemetryData QuickMerge(TelemetryData[] info)
        {
            return info switch
            {
                null => null,
                _ when info.Length == 1 => info[0],
                _ => Merge(info)
            };
        }

        public static void TrackVerbose(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            telemetry.TrackTrace(message, TraceSeverity.Verbose,  QuickMerge(info));
        }

        public static void TrackInformation(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            telemetry.TrackTrace(message, TraceSeverity.Information, QuickMerge(info));
        }

        public static void TrackCritical(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            telemetry.TrackTrace(message, TraceSeverity.Critical, QuickMerge(info));
        }

        public static void TrackWarning(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            telemetry.TrackTrace(message, TraceSeverity.Warning, QuickMerge(info));
        }

        public static void TrackError(this ITelemetry telemetry, string message, params TelemetryData[] info)
        {
            telemetry.TrackTrace(message, TraceSeverity.Error, QuickMerge(info));
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

                foreach (var kv in info.GetDictionary())
                {
                    telemetry[kv.Key] = kv.Value;
                }
            }
        }

        /// <summary>
        /// Generates telemetry metrics
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <param name="metrics"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TelemetryMetrics ToTelemetryMetrics(this Stopwatch stopwatch, TelemetryMetrics metrics = null,
            string prefix = null)
        {
            if (stopwatch == null) throw new ArgumentNullException(nameof(stopwatch));

            return stopwatch.Elapsed.ToTelemetryMetrics(metrics, prefix);
        }

        /// <summary>
        /// Generates telemetry metrics
        /// </summary>
        /// <param name="timespan"></param>
        /// <param name="metrics"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TelemetryMetrics ToTelemetryMetrics(this TimeSpan timespan, TelemetryMetrics metrics = null,
            string prefix = null)
        {
            metrics ??= new TelemetryMetrics();

            metrics[GetKeyWithPrefix("TotalDays")] = timespan.TotalDays;
            metrics[GetKeyWithPrefix("TotalHours")] = timespan.TotalHours;
            metrics[GetKeyWithPrefix("TotalMinutes")] = timespan.TotalMinutes;
            metrics[GetKeyWithPrefix("TotalSeconds")] = timespan.TotalSeconds;
            metrics[GetKeyWithPrefix("TotalSeconds")] = timespan.TotalSeconds;

            return metrics;

            string GetKeyWithPrefix(string key) => string.IsNullOrWhiteSpace(prefix) ? key : $"{prefix}_{key}";
        }


        public static IDependencyCall CreateDependencyCall(this ITelemetry telemetry, string dependencyTypeName,
            string dependencyName, string data,
            DependencyCallDefaultStatus defaultStatus = DependencyCallDefaultStatus.Success)
        {
            return new DependencyCall(telemetry, dependencyTypeName, dependencyName, data, defaultStatus);
        }
    }
}
