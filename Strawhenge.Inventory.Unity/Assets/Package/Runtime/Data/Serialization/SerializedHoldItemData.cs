using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Data
{
    [Serializable]
    public class SerializedHoldItemData : IHoldItemData
    {
        public Vector3 positionOffset;
        public Vector3 rotationOffset;
        public int animationId;
        public int drawFromHammerspaceId;
        public int putInHammerspaceId;
        public int swapFromHandId;

        public Vector3 PositionOffset => positionOffset;

        public Quaternion RotationOffset => Quaternion.Euler(rotationOffset);

        public int AnimationId => animationId;

        public int DrawFromHammerspaceId => drawFromHammerspaceId;

        public int PutInHammerspaceId => putInHammerspaceId;

        public int SwapFromHandId => swapFromHandId;
    }
}