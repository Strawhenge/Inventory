using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    [Serializable]
    public class SerializedHoldItemData : IHoldItemData
    {
        [FormerlySerializedAs("positionOffset"), SerializeField]
        Vector3 _positionOffset;

        [FormerlySerializedAs("rotationOffset"), SerializeField]
        Vector3 _rotationOffset;

        [SerializeField] string[] _animationFlags;

        public Vector3 PositionOffset => _positionOffset;

        public Quaternion RotationOffset => Quaternion.Euler(_rotationOffset);

        public IReadOnlyList<string> AnimationFlags => _animationFlags;
    }
}