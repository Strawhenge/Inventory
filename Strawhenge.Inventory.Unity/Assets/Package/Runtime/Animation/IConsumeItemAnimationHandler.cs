using System;

namespace Strawhenge.Inventory.Unity.Animation
{
    public interface IConsumeItemAnimationHandler
    {
        event Action Consumed;

        void Consume(int animationId, bool invert);

        void Interrupt();
    }
}