using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public class NullHoldItemData : IHoldItemData
    {
        public Vector3 PositionOffset => Vector3.zero;

        public Quaternion RotationOffset => Quaternion.Euler(Vector3.zero);

        public int AnimationId => 0;

        public int DrawFromHammerspaceId => 0;

        public int PutInHammerspaceId => 0;

        public int SwapFromHandId => 0;
    }
}
