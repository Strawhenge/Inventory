using FunctionalUtilities;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.HoldItemData
{
    public interface IHoldItemData
    {
        Vector3 PositionOffset { get; }

        Quaternion RotationOffset { get; }

        Maybe<int> HoldItemAnimation { get; }
    }
}