using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.Data;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public abstract class HandScript : MonoBehaviour
    {
        public IHoldItemAnimationHandler AnimationHandler { private get; set; }

        public PositionAndRotation GetItemDropPoint() => transform.GetPositionAndRotation();

        public void SetItem(ItemHelper item, IHoldItemData data)
        {
            var itemScript = item.Spawn();
            var itemTransform = itemScript.transform;
            itemTransform.parent = transform;
            itemTransform.localPosition = data.PositionOffset;
            itemTransform.localRotation = data.RotationOffset;

            AnimationHandler.Hold(data.AnimationId);
        }

        public void UnsetItem()
        {
            AnimationHandler.Unhold();
        }
    }
}