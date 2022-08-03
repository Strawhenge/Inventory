using Strawhenge.Inventory.Unity.Animation;
using System;
using UnityEngine;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HandComponents : IHandComponents
    {
        readonly IHoldItemAnimationHandler holdItemAnimationHandler;
        readonly ILogger logger;

        IHandComponent left;
        IHandComponent right;

        public HandComponents(IHoldItemAnimationHandler holdItemAnimationHandler, ILogger logger)
        {
            this.holdItemAnimationHandler = holdItemAnimationHandler;
            this.logger = logger;
        }

        public IHandComponent Left => left ?? throw new InvalidOperationException($"'{nameof(HandComponents)}' has not been initialized.");

        public IHandComponent Right => right ?? throw new InvalidOperationException($"'{nameof(HandComponents)}' has not been initialized.");

        public void Initialize(Transform left, Transform right)
        {
            this.left = new HandComponent(holdItemAnimationHandler, left, logger, x => x.Data.LeftHandHoldData);
            this.right = new HandComponent(holdItemAnimationHandler, right, logger, x => x.Data.RightHandHoldData);
        }
    }
}