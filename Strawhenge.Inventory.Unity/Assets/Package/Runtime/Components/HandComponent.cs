using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Items;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HandComponent : IHandComponent
    {
        private readonly IHoldItemAnimationHandler animationHandler;
        private readonly Transform transform;
        private readonly ILogger logger;
        private readonly Func<IItemHelper, IHoldItemData> getHoldItemData;

        public HandComponent(IHoldItemAnimationHandler animationHandler, Transform transform, ILogger logger, Func<IItemHelper, IHoldItemData> getHoldItemData)
        {
            this.animationHandler = animationHandler;
            this.transform = transform;
            this.logger = logger;
            this.getHoldItemData = getHoldItemData;
        }

        public Maybe<IItemHelper> Item { get; private set; } = Maybe.None<IItemHelper>();

        public void SetItem(IItemHelper item)
        {
            var holdData = getHoldItemData(item);

            var itemScript = item.Spawn();
            itemScript.transform.parent = transform;
            itemScript.transform.localPosition = holdData.PositionOffset;
            itemScript.transform.localRotation = holdData.RotationOffset;

            animationHandler.Hold(holdData.AnimationId);

            Item = Maybe.Some(item);
        }

        public IItemHelper TakeItem()
        {
            animationHandler.Unhold();

            var item = Item.Reduce(() =>
            {
                logger.LogError("No item in hand.");
                return new NullItemHelper();
            });

            Item = Maybe.None<IItemHelper>();

            item.Spawn().transform.parent = null;

            return item;
        }
    }
}