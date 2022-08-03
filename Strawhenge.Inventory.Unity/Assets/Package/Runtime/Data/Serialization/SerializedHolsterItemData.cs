using Strawhenge.Inventory.Unity.Data.ScriptableObjects;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Data
{
    [Serializable]
    public class SerializedHolsterItemData : IHolsterItemData
    {
        [SerializeField] HolsterScriptableObject holster;
        [SerializeField] Vector3 positionOffset;
        [SerializeField] Vector3 rotationOffset;
        [SerializeField] int drawFromHolsterRightHandId;
        [SerializeField] int putInHolsterRightHandId;
        [SerializeField] int drawFromHolsterLeftHandId;
        [SerializeField] int putInHolsterLeftHandId;

        public string HolsterName => holster.Name;

        public Vector3 PositionOffset => positionOffset;

        public Quaternion RotationOffset => Quaternion.Euler(rotationOffset);

        public int DrawFromHolsterRightHandId => drawFromHolsterRightHandId;

        public int PutInHolsterRightHandId => putInHolsterRightHandId;

        public int DrawFromHolsterLeftHandId => drawFromHolsterLeftHandId;

        public int PutInHolsterLeftHandId => putInHolsterLeftHandId;
    }
}