using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public static class GameObjectExtensions
    {
        public static void SetLayerIncludingChildren(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;

            foreach (var child in gameObject.GetComponentsInChildren<Component>())
                child.gameObject.layer = layer;
        }
    }
}
