using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public static class ComponentExtensions
    {
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            if (component.TryGetComponent<T>(out var addedComponent))
                return addedComponent;

            return component.gameObject.AddComponent<T>();
        }
    }
}
