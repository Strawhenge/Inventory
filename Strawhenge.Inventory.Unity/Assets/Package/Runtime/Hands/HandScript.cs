using FunctionalUtilities;
using Strawhenge.Common.Unity;
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

        public PositionAndRotation GetItemDropPoint() => transform.GetPositionAndRotation();

        public void SetItem(ItemHelper item, IHoldItemData data)
        {
            var itemScript = item.Spawn();
            var itemTransform = itemScript.transform;
            itemTransform.parent = transform;
            itemTransform.localPosition = data.PositionOffset;
            itemTransform.localRotation = data.RotationOffset;

            AnimationHandler.Hold(data.AnimationId);

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
    }
}