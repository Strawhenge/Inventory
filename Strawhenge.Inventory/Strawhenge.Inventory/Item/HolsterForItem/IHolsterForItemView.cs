using System;

namespace Strawhenge.Inventory.Items.HolsterForItem
{
    public interface IHolsterForItemView
    {
        event Action Released;

        void DrawLeftHand(Action callback = null);

        void DrawRightHand(Action callback = null);

        void PutAwayLeftHand(Action callback = null);

        void PutAwayRightHand(Action callback = null);

        void Show(Action callback = null);

        void Hide(Action callback = null);

        void Drop(Action callback = null);
    }
}