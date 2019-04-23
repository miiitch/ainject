using System;
using System.Collections.Generic;

namespace Ainject.Abstractions
{
    public class TelemetryInfo<T>
    {
        private readonly Dictionary<string, T> _dictionary;

        public Dictionary<string, T> GetDictionary() => new Dictionary<string, T>(_dictionary);


        public TelemetryInfo() : this(null)
        {

        }

        public TelemetryInfo(IDictionary<string, T> values)
        {
            if (values is null)
            {
                _dictionary = new Dictionary<string, T>();
            }
            else
            {
                _dictionary = new Dictionary<string, T>(values);
            }                
        }

        private void AddKeyValue(string key, T value)
        {
            if (_dictionary.ContainsKey(key))
            {
                return;
            }
            _dictionary.Add(key,value);
        }

        public T this[string key]
        {
            get => _dictionary[key];
            set => AddKeyValue(key, value);
        }

        public bool IsEmpty => _dictionary.Count == 0;

        public void CopyTo(Dictionary<string, T> target)
        {
            if (target is null) throw new ArgumentNullException(nameof(target));

            foreach (var kv in _dictionary)
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