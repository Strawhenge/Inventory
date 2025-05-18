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
    }
}