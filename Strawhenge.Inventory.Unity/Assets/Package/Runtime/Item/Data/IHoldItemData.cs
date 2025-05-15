using System.Collections.Generic;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public interface IHoldItemData
    {
        Vector3 PositionOffset { get; }

        Quaternion RotationOffset { get; }

        IReadOnlyList<string> AnimationFlags { get; }
    }
}