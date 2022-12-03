using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Strawhenge.Inventory.Unity.Data
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

        [FormerlySerializedAs("drawFromHolsterRightHandId"), SerializeField] 
        int _drawFromHolsterRightHandId;

        [FormerlySerializedAs("putInHolsterRightHandId"), SerializeField] 
        int _putInHolsterRightHandId;

        [FormerlySerializedAs("drawFromHolsterLeftHandId"), SerializeField] 
        int _drawFromHolsterLeftHandId;

        [FormerlySerializedAs("putInHolsterLeftHandId"), SerializeField] 
        int _putInHolsterLeftHandId;

        public string HolsterName => _holster.Name;

        public Vector3 PositionOffset => _positionOffset;

        public Quaternion RotationOffset => Quaternion.Euler(_rotationOffset);

        public int DrawFromHolsterRightHandId => _drawFromHolsterRightHandId;

        public int PutInHolsterRightHandId => _putInHolsterRightHandId;

        public int DrawFromHolsterLeftHandId => _drawFromHolsterLeftHandId;

        public int PutInHolsterLeftHandId => _putInHolsterLeftHandId;
    }
}