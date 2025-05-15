using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.Data
{
    public class NullHolsterItemData : IHolsterItemData
    {
        public static IHolsterItemData Instance { get; } = new NullHolsterItemData();

        NullHolsterItemData()
        {
        }

        public Vector3 PositionOffset => Vector3.zero;

        public Quaternion RotationOffset => Quaternion.Euler(Vector3.zero);

        public IDrawAnimationSettings DrawAnimationSettings => NullDrawAnimationSettings.Instance;

        public int DrawFromHolsterRightHandId => 0;

        public int PutInHolsterRightHandId => 0;

        public int DrawFromHolsterLeftHandId => 0;

        public int PutInHolsterLeftHandId => 0;
    }
}