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

        public Maybe<ItemHelper> Item { get; private set; } = Maybe.None<ItemHelper>();

        public void SetItem(ItemHelper item)
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

        public Maybe<ItemHelper> TakeItem()
        {
            AnimationHandler.Unhold();

            if (!Item.HasSome(out var item))
            {
                Debug.LogError("No item in hand.");
                return Maybe.None<ItemHelper>();
            }

            Item = Maybe.None<ItemHelper>();
            item.Spawn().transform.parent = null;
            
            Removed?.Invoke();
            return Maybe.Some(item);
        }

        public abstract IHoldItemData GetHoldItemData(IItemData itemData);
    }
}