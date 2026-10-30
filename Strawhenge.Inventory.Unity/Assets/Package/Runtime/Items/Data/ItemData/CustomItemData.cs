using FunctionalUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.ItemData
{
    /// <summary>
    /// The scriptable objects in an item's Custom Data list, looked up by type.
    /// Read it with <see cref="CustomItemDataExtensions.GetCustomData{T}(Strawhenge.Inventory.Items.Item)"/>.
    /// </summary>
    public sealed class CustomItemData
    {
        readonly ScriptableObject[] _data;
        readonly string _itemName;
        readonly UnityEngine.Object _context;
        readonly Dictionary<Type, ScriptableObject> _cache = new();

        internal CustomItemData(IEnumerable<ScriptableObject> data, string itemName, UnityEngine.Object context)
        {
            // Unity's == null also catches unassigned and destroyed references, which "is" would treat as real objects.
            _data = (data ?? Enumerable.Empty<ScriptableObject>())
                .Where(x => x != null)
                .ToArray();

            _itemName = itemName;
            _context = context;

            WarnOnDuplicateTypes();
        }

        /// <summary>
        /// The first entry that is a <typeparamref name="T"/>, including subclasses and implementations of
        /// interfaces. Logs a warning, once per type, if more than one entry matches.
        /// </summary>
        public Maybe<T> Get<T>() where T : class
        {
            var type = typeof(T);

            if (!_cache.TryGetValue(type, out var match))
            {
                match = Find<T>();
                _cache[type] = match; // Misses are cached as null, so each type is only searched once.
            }

            return match is T value
                ? Maybe.Some(value)
                : Maybe.None<T>();
        }

        ScriptableObject Find<T>() where T : class
        {
            ScriptableObject first = null;

            foreach (var data in _data)
            {
                if (data is not T)
                    continue;

                if (first == null)
                {
                    first = data;
                    continue;
                }

                Debug.LogWarning(
                    $"Item '{_itemName}' has more than one custom data matching '{typeof(T).Name}'. " +
                    $"Using '{first.name}'.",
                    _context);
                break;
            }

            return first;
        }

        void WarnOnDuplicateTypes()
        {
            var duplicateTypes = _data
                .GroupBy(x => x.GetType())
                .Where(group => group.Count() > 1)
                .Select(group => group.Key);

            foreach (var type in duplicateTypes)
            {
                Debug.LogWarning(
                    $"Item '{_itemName}' has more than one custom data of type '{type.Name}'. " +
                    "Only the first is used.",
                    _context);
            }
        }
    }
}
