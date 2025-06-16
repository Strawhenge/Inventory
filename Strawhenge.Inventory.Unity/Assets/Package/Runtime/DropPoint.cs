using Strawhenge.Common.Unity;
using UnityEngine;

namespace Strawhenge.Inventory.Unity
{
    class DropPoint
    {
        readonly Transform _transform;

        public DropPoint(Transform transform)
        {
            _transform = transform;
        }

        public PositionAndRotation GetPoint()
        {
            return new PositionAndRotation()
            {
                Position = _transform.position + _transform.forward + Vector3.up,
                Rotation = _transform.rotation
            };
        }
    }
}