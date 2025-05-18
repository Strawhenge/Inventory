using Strawhenge.Common.Unity.Serialization;
using Strawhenge.Inventory.Unity.Items.Data.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    [Serializable]
    public class SerializedHolsterItemData : IHolsterItemData
    {
        [FormerlySerializedAs("holster"), SerializeField]
        HolsterScriptableObject _holster;

        [FormerlySerializedAs("positionOffset"), SerializeField]
        Vector3 _positionOffset;

        [FormerlySerializedAs("rotationOffset"), SerializeField]
        Vector3 _rotationOffset;

        [SerializeField] SerializedSource<
            IDrawAnimationSettings,
            SerializedDrawAnimationSettings,
            DrawAnimationSettingsScriptableObject> _drawAnimationSettings;

        public string HolsterName => _holster.Name;

        public Vector3 PositionOffset => _positionOffset;

        public Quaternion RotationOffset => Quaternion.Euler(_rotationOffset);

        public IDrawAnimationSettings DrawAnimationSettings =>
            _drawAnimationSettings.GetValueOrDefault(
                () => NullDrawAnimationSettings.Instance);
    }
}