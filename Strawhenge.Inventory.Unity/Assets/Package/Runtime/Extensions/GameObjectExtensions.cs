using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent<T>(out var component))
                return component;

            return gameObject.AddComponent<T>();
        }

        public static void SetLayerIncludingChildren(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;

            foreach (var child in gameObject.GetComponentsInChildren<Component>())
                child.gameObject.layer = layer;
        }
    }
}
