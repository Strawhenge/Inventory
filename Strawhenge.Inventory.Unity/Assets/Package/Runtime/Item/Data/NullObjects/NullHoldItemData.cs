using System;
using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public class NullHoldItemData : IHoldItemData
    {
        public static IHoldItemData Instance { get; } = new NullHoldItemData();

        NullHoldItemData()
        {
        }

        public Vector3 PositionOffset => Vector3.zero;

        public Quaternion RotationOffset => Quaternion.Euler(Vector3.zero);

        public IHoldAnimationSettings AnimationSettings => NullHoldAnimationSettings.Instance;
    }
}