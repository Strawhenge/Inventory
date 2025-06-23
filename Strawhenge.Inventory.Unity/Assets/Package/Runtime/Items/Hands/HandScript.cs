using Strawhenge.Common.Unity;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.HoldItemData;
using UnityEngine;
using UnityEngine.Events;

namespace Strawhenge.Inventory.Unity.Items
{
    public abstract class HandScript : MonoBehaviour
    {
        [SerializeField] UnityEvent<ItemScript> _itemSet;
        [SerializeField] UnityEvent _itemUnset;

        IHoldItemData _holdItemData;

        internal HoldItemAnimationHandler AnimationHandler { private get; set; }

        internal PositionAndRotation GetItemDropPoint() => transform.GetPositionAndRotation();

        internal void SetItem(ItemScriptInstance item, IHoldItemData data)
        {
            if (_holdItemData != null)
                AnimationHandler.Unhold(_holdItemData.AnimationSettings.AnimationFlags);

            _holdItemData = data;

            var itemScript = item.Spawn();
            var itemTransform = itemScript.transform;
            itemTransform.parent = transform;
            itemTransform.localPosition = data.PositionOffset;
            itemTransform.localRotation = data.RotationOffset;

            AnimationHandler.Hold(_holdItemData.AnimationSettings.AnimationFlags);
            _itemSet.Invoke(itemScript);
        }

        internal void UnsetItem()
        {
            if (_holdItemData != null)
                AnimationHandler.Unhold(_holdItemData.AnimationSettings.AnimationFlags);

            _itemUnset.Invoke();
        }
    }
}