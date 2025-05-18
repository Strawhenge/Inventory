using Strawhenge.Common.Unity.Serialization;
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

        [SerializeField] SerializedSource<
            IHoldAnimationSettings,
            SerializedHoldAnimationSettings,
            HoldAnimationSettingsScriptableObject> _animationSettings;

        public Vector3 PositionOffset => _positionOffset;

        public Quaternion RotationOffset => Quaternion.Euler(_rotationOffset);

        public IHoldAnimationSettings AnimationSettings =>
            _animationSettings
                .GetValueOrDefault(() => NullHoldAnimationSettings.Instance);
    }
}