using System;
using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory
{
    public class Context
    {
        readonly Dictionary<Type, object> _data = new Dictionary<Type, object>();
        
        public void Set<T>(T value) where T : class
        {
            _data[typeof(T)] = value;
        }

        public Maybe<T> Get<T>() where T : class =>
            _data.TryGetValue(typeof(T), out var rawValue) && rawValue is T value
                ? value
                : Maybe.None<T>();
    }
}