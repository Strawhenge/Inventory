using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.ItemData
{
    /// <summary>
    /// Adds an item's Custom Data to its data, so each entry can be read back with <c>item.Get&lt;T&gt;()</c>,
    /// where <c>T</c> is the entry's type, one of its base types, or one of its interfaces.
    /// </summary>
    static class CustomItemDataSetter
    {
        static readonly MethodInfo SetMethod = typeof(IDataSetter).GetMethod(nameof(IDataSetter.Set));

        internal static void Set(
            IDataSetter dataSetter,
            IEnumerable<ScriptableObject> customData,
            ICollection<Type> reservedTypes,
            string itemName,
            UnityEngine.Object context)
        {
            if (customData == null)
                return;

            var registered = new Dictionary<Type, ScriptableObject>();

            foreach (var data in customData)
            {
                // Unity's == null also catches unassigned and destroyed references.
                if (data == null)
                    continue;

                foreach (var type in GetTypes(data.GetType()))
                {
                    if (reservedTypes.Contains(type))
                    {
                        Debug.LogWarning(
                            $"Item '{itemName}': custom data '{data.name}' is a '{type.Name}', which is reserved " +
                            "for the item's built-in data. It won't be returned for that type.",
                            context);
                        continue;
                    }

                    if (registered.TryGetValue(type, out var first))
                    {
                        Debug.LogWarning(
                            $"Item '{itemName}': custom data '{first.name}' and '{data.name}' are both a " +
                            $"'{type.Name}'. Get<{type.Name}>() returns '{first.name}'.",
                            context);
                        continue;
                    }

                    registered[type] = data;

                    // IDataSetter stores values by the generic type argument, so call it with this type.
                    SetMethod.MakeGenericMethod(type).Invoke(dataSetter, new object[] { data });
                }
            }
        }

        /// <summary>
        /// The type, its base types up to (not including) <see cref="ScriptableObject"/>, and its interfaces,
        /// leaving out Unity and .NET types such as <see cref="ISerializationCallbackReceiver"/>.
        /// </summary>
        static IEnumerable<Type> GetTypes(Type type)
        {
            for (var current = type; current != null && current != typeof(ScriptableObject); current = current.BaseType)
            {
                if (!IsUnityOrSystemType(current))
                    yield return current;
            }

            foreach (var @interface in type.GetInterfaces())
            {
                if (!IsUnityOrSystemType(@interface))
                    yield return @interface;
            }
        }

        static bool IsUnityOrSystemType(Type type)
        {
            var ns = type.Namespace ?? string.Empty;

            return IsNamespace(ns, "System")
                   || IsNamespace(ns, "UnityEngine")
                   || IsNamespace(ns, "UnityEditor")
                   || IsNamespace(ns, "Unity");
        }

        static bool IsNamespace(string ns, string root) =>
            ns == root || ns.StartsWith(root + ".", StringComparison.Ordinal);
    }
}
