using System;

namespace Strawhenge.Inventory.TransientItems
{
    public interface ITransientItemHolder
    {
        void HoldLeftHand(string itemName, Action callback = null);

        void HoldRightHand(string itemName, Action callback = null);

        void Unhold(Action callback);
    }
}