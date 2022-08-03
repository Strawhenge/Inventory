using UnityEngine;

namespace Strawhenge.Inventory.Unity.Data
{
    public interface IHoldItemData
    {
        Vector3 PositionOffset { get; }

        Quaternion RotationOffset { get; }

        int AnimationId { get; }

        int DrawFromHammerspaceId { get; }

        int PutInHammerspaceId { get; }

        int SwapFromHandId { get; }
    }
}
