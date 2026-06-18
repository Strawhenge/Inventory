using FunctionalUtilities;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items.HoldItemData
{
    [Serializable]
    public class SerializedHoldItemData : IHoldItemData
    {
        [FormerlySerializedAs("positionOffset"), SerializeField]
        Vector3 _positionOffset;

        [FormerlySerializedAs("rotationOffset"), SerializeField]
        Vector3 _rotationOffset;

        [SerializeField] HoldItemAnimationScriptableObject _holdItemAnimation;

        public Vector3 PositionOffset => _positionOffset;

        public Quaternion RotationOffset => Quaternion.Euler(_rotationOffset);

        public Maybe<int> HoldItemAnimation => _holdItemAnimation != null
            ? Maybe.Some(_holdItemAnimation.Id)
            : Maybe.None<int>();
    }
}