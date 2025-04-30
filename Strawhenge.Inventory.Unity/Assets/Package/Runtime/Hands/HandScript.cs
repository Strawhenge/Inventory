using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Strawhenge.Inventory.Unity.Items
{
    public abstract class HandScript : MonoBehaviour
    {
        [SerializeField] UnityEvent<ItemScript> _itemSet;
        [SerializeField] UnityEvent _itemUnset;

        public HoldItemAnimationHandler AnimationHandler { private get; set; }

        public PositionAndRotation GetItemDropPoint() => transform.GetPositionAndRotation();

        public void SetItem(ItemScriptInstance item, IHoldItemData data)
        {
            var itemScript = item.Spawn();
            var itemTransform = itemScript.transform;
            itemTransform.parent = transform;
            itemTransform.localPosition = data.PositionOffset;
            itemTransform.localRotation = data.RotationOffset;

            AnimationHandler.Hold(data.AnimationId);
            _itemSet.Invoke(itemScript);
        }

        public void UnsetItem()
        {
            AnimationHandler.Unhold();
            _itemUnset.Invoke();
        }
    }
}