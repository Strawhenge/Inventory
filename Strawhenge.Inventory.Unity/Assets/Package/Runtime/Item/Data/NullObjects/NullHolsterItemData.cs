using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public class NullHolsterItemData : IHolsterItemData
    {
        public NullHolsterItemData(string holsterName)
        {
            HolsterName = holsterName;
        }

        public string HolsterName { get; }

        public Vector3 PositionOffset => Vector3.zero;

        public Quaternion RotationOffset => Quaternion.Euler(Vector3.zero);

        public int DrawFromHolsterRightHandId => 0;

        public int PutInHolsterRightHandId => 0;

        public int DrawFromHolsterLeftHandId => 0;

        public int PutInHolsterLeftHandId => 0;
    }
}