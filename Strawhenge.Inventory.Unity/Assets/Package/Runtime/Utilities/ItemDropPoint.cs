using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class ItemDropPoint : IItemDropPoint
    {
        private readonly Transform transform;

        public ItemDropPoint(Transform transform)
        {
            this.transform = transform;
        }

        public Vector3 GetPoint()
        {
            return transform.position +
                transform.forward +
                Vector3.up;
        }
    }
}

