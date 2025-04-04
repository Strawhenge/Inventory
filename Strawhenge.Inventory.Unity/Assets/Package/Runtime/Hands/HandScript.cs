using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Items.Data;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Items
{
    public abstract class HandScript : MonoBehaviour
    {
        public event Action<ItemScript> Added;
        public event Action Removed;

        public IHoldItemAnimationHandler AnimationHandler { private get; set; }

        public Maybe<IItemHelper> Item { get; private set; } = Maybe.None<IItemHelper>();

        public void SetItem(IItemHelper item)
        {
            var holdData = GetHoldItemData(item.Data);

            var itemScript = item.Spawn();
            var itemTransform = itemScript.transform;
            itemTransform.parent = transform;
            itemTransform.localPosition = holdData.PositionOffset;
            itemTransform.localRotation = holdData.RotationOffset;

            AnimationHandler.Hold(holdData.AnimationId);

            Item = Maybe.Some(item);

            Added?.Invoke(itemScript);
        }

        public IItemHelper TakeItem()
        {
            AnimationHandler.Unhold();

            var item = Item.Reduce(() =>
            {
                Debug.LogError("No item in hand.");
                return new NullItemHelper();
            });

            Item = Maybe.None<IItemHelper>();

            item.Spawn().transform.parent = null;

            Removed?.Invoke();
            return item;
        }

        public abstract IHoldItemData GetHoldItemData(IItemData itemData);
    }
}