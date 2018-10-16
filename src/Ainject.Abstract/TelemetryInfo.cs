using System;
using System.Collections.Generic;

namespace Ainject.Abstract
{
    public class TelemetryInfo<T>
    {
        public Dictionary<string, T> Dictionary { get; }

        public TelemetryInfo()
        {
            Dictionary = new Dictionary<string, T>();
        }

        public T this[string key]
        {
            get => Dictionary[key];
            set => Dictionary[key] = value;
        }

        public bool IsEmpty => Dictionary.Count == 0;

        public void CopyTo(TelemetryInfo<T> target)
        {
            CopyTo(target.Dictionary);
        }

        public void CopyTo(Dictionary<string, T> target)
        {
            if (target is null) throw new ArgumentNullException(nameof(target));

            foreach (var kv in Dictionary)
            {
                target[kv.Key] = kv.Value;
            }
        }

        public void Append(Dictionary<string, T> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            foreach (var kv in source)
            {
                this[kv.Key] = kv.Value;
            }
        }


    }
}