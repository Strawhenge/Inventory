using System;

namespace Strawhenge.Inventory.Unity.Animation
{
    public interface IProduceItemAnimationHandler
    {
        event Action DrawEnded;
        event Action GrabItem;
        event Action PutAwayEnded;
        event Action ReleaseItem;

        void DrawItem(int animationId);
        void PutAwayItem(int animationId);
        void Interupt();
    }
}