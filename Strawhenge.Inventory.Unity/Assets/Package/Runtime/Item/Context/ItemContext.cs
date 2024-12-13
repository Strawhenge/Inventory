using FunctionalUtilities;
using System;
using System.Collections.Generic;

namespace Strawhenge.Inventory.Unity.Items.Context
{
    public class ItemContext
    {
        readonly Dictionary<Type, object> _objects = new();

        public void Add<T>(T obj)
        {
            _objects[typeof(T)] = obj;
        }

        public Maybe<T> Get<T>()
        {
            return _objects.TryGetValue(typeof(T), out var result)
                ? Maybe.Some((T)result)
                : Maybe.None<T>();
        }
    }
}