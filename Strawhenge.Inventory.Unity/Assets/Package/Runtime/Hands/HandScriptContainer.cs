using Strawhenge.Inventory.Unity.Items;
using System;

namespace Strawhenge.Inventory.Unity.Components
{
    public class HandScriptContainer
    {
        HandScript _left;
        HandScript _right;

        public event Action Initialized;

        public HandScript Left =>
            _left ?? throw new InvalidOperationException($"'{nameof(HandScriptContainer)}' has not been initialized.");

        public HandScript Right =>
            _right ?? throw new InvalidOperationException($"'{nameof(HandScriptContainer)}' has not been initialized.");

        public void Initialize(LeftHandScript left, RightHandScript right)
        {
            _left = left;
            _right = right;

            Initialized?.Invoke();
        }
    }
}