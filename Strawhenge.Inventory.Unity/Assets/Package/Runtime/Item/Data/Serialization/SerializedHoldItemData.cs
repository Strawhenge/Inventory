using System;
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

        [FormerlySerializedAs("animationId"), SerializeField] 
        int _animationId;

        [FormerlySerializedAs("drawFromHammerspaceId"), SerializeField] 
        int _drawFromHammerspaceId;

        [FormerlySerializedAs("putInHammerspaceId"), SerializeField] 
        int _putInHammerspaceId;

        public Vector3 PositionOffset => _positionOffset;

        public Quaternion RotationOffset => Quaternion.Euler(_rotationOffset);

        public int AnimationId => _animationId;

        public int DrawFromHammerspaceId => _drawFromHammerspaceId;

        public int PutInHammerspaceId => _putInHammerspaceId;
    }
}