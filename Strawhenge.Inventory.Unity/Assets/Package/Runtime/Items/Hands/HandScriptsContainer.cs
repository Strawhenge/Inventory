using System;

namespace Strawhenge.Inventory.Unity.Items
{
    public class HandScriptsContainer
    {
        HandScript _left;
        HandScript _right;

        public event Action Initialized;

        public HandScript Left =>
            _left ?? throw new InvalidOperationException($"'{nameof(HandScriptsContainer)}' has not been initialized.");

        public HandScript Right =>
            _right ?? throw new InvalidOperationException($"'{nameof(HandScriptsContainer)}' has not been initialized.");

        public void Initialize(LeftHandScript left, RightHandScript right)
        {
            _left = left;
            _right = right;

            Initialized?.Invoke();
        }
    }
}