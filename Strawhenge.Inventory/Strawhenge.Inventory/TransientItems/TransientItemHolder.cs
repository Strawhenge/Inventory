using System;

namespace Strawhenge.Inventory.TransientItems
{
    public class TransientItemHolder : ITransientItemHolder
    {
        private readonly ITransientItemLocator transientItemLocator;

        IItem item;

        public TransientItemHolder(ITransientItemLocator transientItemLocator)
        {
            this.transientItemLocator = transientItemLocator;
        }

        public void HoldLeftHand(string itemName, Action callback)
        {
            if (transientItemLocator.GetItemByName(itemName).HasSome(out item))
                item.HoldLeftHand(callback);
            else
                callback();
        }

        public void HoldRightHand(string itemName, Action callback)
        {
            if (transientItemLocator.GetItemByName(itemName).HasSome(out item))
                item.HoldRightHand(callback);
            else
                callback();
        }

        public void Unhold(Action callback)
        {
            if (item == null)
                callback();
            else
                item.ClearFromHands(callback);
        }
    }
}