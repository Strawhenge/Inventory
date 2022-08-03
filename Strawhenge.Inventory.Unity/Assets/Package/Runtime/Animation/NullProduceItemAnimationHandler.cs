using System;

namespace Strawhenge.Inventory.Unity.Animation
{
    public class NullProduceItemAnimationHandler : IProduceItemAnimationHandler
    {
        public event Action DrawEnded;
        public event Action PutAwayEnded;

        event Action IProduceItemAnimationHandler.GrabItem
        {
            add
            {                
            }

            remove
            {              
            }
        }

        event Action IProduceItemAnimationHandler.ReleaseItem
        {
            add
            {
            }

            remove
            {
            }
        }

        public void DrawItem(int animationId)
        {
            DrawEnded?.Invoke();
        }

        public void Interupt()
        {
        }

        public void PutAwayItem(int animationId)
        {
            PutAwayEnded?.Invoke();
        }
    }
}