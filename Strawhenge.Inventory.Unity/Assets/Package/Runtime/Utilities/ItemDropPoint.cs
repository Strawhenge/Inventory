using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    public class ItemDropPoint : IItemDropPoint
    {
        readonly Transform _transform;

        public ItemDropPoint(Transform transform)
        {
            _transform = transform;
        }

        public Vector3 GetPoint()
        {
            return _transform.position +
                _transform.forward +
                Vector3.up;
        }
    }
}

