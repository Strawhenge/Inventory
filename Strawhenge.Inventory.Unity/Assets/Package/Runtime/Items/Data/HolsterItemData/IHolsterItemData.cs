using Strawhenge.Inventory.Unity.Items.DrawAnimationSettings;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.HolsterItemData
{
    public interface IHolsterItemData
    {
        Vector3 PositionOffset { get; }

        Quaternion RotationOffset { get; }

        IDrawAnimationSettings DrawAnimationSettings { get; }
    }
}