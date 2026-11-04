using System;
using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Common.Logging;

namespace Strawhenge.Inventory
{
    class GenericData : IDataSetter
    {
        readonly Dictionary<Type, object> _data = new Dictionary<Type, object>();
        readonly List<object> _addedData = new List<object>();
        readonly Dictionary<Type, object> _addedDataMatches = new Dictionary<Type, object>();
        readonly string _name;
        readonly Maybe<ILogger> _logger;

        public GenericData() : this(string.Empty, Maybe.None<ILogger>())
        {
        }

        public GenericData(string name, Maybe<ILogger> logger)
        {
            _name = name;
            _logger = logger;
        }

        public void Set<T>(T value) where T : class
        {
            _data[typeof(T)] = value;
        }

        public void Add(object value)
        {
            if (value == null)
                return;

            _addedData.Add(value);
            _addedDataMatches.Clear();
        }

        public Maybe<T> Get<T>() where T : class
        {
            if (_data.TryGetValue(typeof(T), out var rawValue) && rawValue is T value)
                return Maybe.Some(value);

            // Each type is only searched for once. Misses are cached as null.
            if (!_addedDataMatches.TryGetValue(typeof(T), out var match))
            {
                match = FindAddedData<T>();
                _addedDataMatches[typeof(T)] = match;
            }

            return match is T addedValue
                ? Maybe.Some(addedValue)
                : Maybe.None<T>();
        }

        object FindAddedData<T>() where T : class
        {
            object first = null;
            var matchCount = 0;

            foreach (var data in _addedData)
            {
                if (!(data is T))
                    continue;

                matchCount++;

                if (first == null)
                    first = data;
            }

            if (matchCount > 1)
            {
                _logger.Do(logger => logger.LogWarning(
                    $"{_name}: {matchCount} data values are a '{typeof(T).Name}'. " +
                    $"Get<{typeof(T).Name}>() returns the first, '{first}'."));
            }

            return first;
        }
    }
}