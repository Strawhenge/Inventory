using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IHolsterItemData
    {
        Vector3 PositionOffset { get; }

        Quaternion RotationOffset { get; }

        IDrawAnimationSettings DrawAnimationSettings { get; }
    }
}