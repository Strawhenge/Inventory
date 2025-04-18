﻿using Strawhenge.Common.Unity;
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