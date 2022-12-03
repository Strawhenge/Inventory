using Strawhenge.Inventory.Unity.Animation;
using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Items;
using System;
using FunctionalUtilities;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HandComponent : IHandComponent
    {
        readonly IHoldItemAnimationHandler _animationHandler;
        readonly Transform _transform;
        readonly ILogger _logger;
        readonly Func<IItemHelper, IHoldItemData> _getHoldItemData;

        public HandComponent(IHoldItemAnimationHandler animationHandler, Transform transform, ILogger logger,
            Func<IItemHelper, IHoldItemData> getHoldItemData)
        {
            _animationHandler = animationHandler;
            _transform = transform;
            _logger = logger;
            _getHoldItemData = getHoldItemData;
        }

        public Maybe<IItemHelper> Item { get; private set; } = Maybe.None<IItemHelper>();

        public void SetItem(IItemHelper item)
        {
            var holdData = _getHoldItemData(item);

            var itemScript = item.Spawn();
            var itemTransform = itemScript.transform;
            itemTransform.parent = _transform;
            itemTransform.localPosition = holdData.PositionOffset;
            itemTransform.localRotation = holdData.RotationOffset;

            _animationHandler.Hold(holdData.AnimationId);

            Item = Maybe.Some(item);
        }

        public IItemHelper TakeItem()
        {
            _animationHandler.Unhold();

            var item = Item.Reduce(() =>
            {
                _logger.LogError("No item in hand.");
                return new NullItemHelper();
            });

            Item = Maybe.None<IItemHelper>();

            item.Spawn().transform.parent = null;

            return item;
        }
    }
}