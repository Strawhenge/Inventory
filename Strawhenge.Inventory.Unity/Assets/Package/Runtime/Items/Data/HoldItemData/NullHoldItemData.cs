using Strawhenge.Inventory.Unity.Items.HoldAnimationSettings;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items.HoldItemData
{
    public class NullHoldItemData : IHoldItemData
    {
        public static IHoldItemData Instance { get; } = new NullHoldItemData();

        NullHoldItemData()
        {
        }

        public Vector3 PositionOffset => Vector3.zero;

        public Quaternion RotationOffset => Quaternion.Euler(Vector3.zero);

        public IHoldAnimationSettings AnimationSettings => NullHoldAnimationSettings.Instance;
    }
}