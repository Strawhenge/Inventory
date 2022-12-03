using Strawhenge.Inventory.Unity.Animation;
using System;
using UnityEngine;
using ILogger = Strawhenge.Common.Logging.ILogger;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HandComponents : IHandComponents
    {
        readonly IHoldItemAnimationHandler _holdItemAnimationHandler;
        readonly ILogger _logger;

        IHandComponent _left;
        IHandComponent _right;

        public HandComponents(IHoldItemAnimationHandler holdItemAnimationHandler, ILogger logger)
        {
            _holdItemAnimationHandler = holdItemAnimationHandler;
            _logger = logger;
        }

        public IHandComponent Left => _left ?? throw new InvalidOperationException($"'{nameof(HandComponents)}' has not been initialized.");

        public IHandComponent Right => _right ?? throw new InvalidOperationException($"'{nameof(HandComponents)}' has not been initialized.");

        public void Initialize(Transform left, Transform right)
        {
            _left = new HandComponent(_holdItemAnimationHandler, left, _logger, x => x.Data.LeftHandHoldData);
            _right = new HandComponent(_holdItemAnimationHandler, right, _logger, x => x.Data.RightHandHoldData);
        }
    }
}