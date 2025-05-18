using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IHoldItemData
    {
        Vector3 PositionOffset { get; }

        Quaternion RotationOffset { get; }

        IHoldAnimationSettings AnimationSettings { get; }
    }
}