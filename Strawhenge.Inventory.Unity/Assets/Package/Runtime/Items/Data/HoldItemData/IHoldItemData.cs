using Strawhenge.Inventory.Unity.Items.HoldAnimationSettings;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.HoldItemData
{
    public interface IHoldItemData
    {
        Vector3 PositionOffset { get; }

        Quaternion RotationOffset { get; }

        IHoldAnimationSettings AnimationSettings { get; }
    }
}