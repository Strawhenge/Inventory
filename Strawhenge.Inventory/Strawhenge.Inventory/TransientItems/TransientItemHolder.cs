using System;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.TransientItems
{
    public class TransientItemHolder : ITransientItemHolder
    {
        readonly ITransientItemLocator _transientItemLocator;

        Item _item;

        public TransientItemHolder(ITransientItemLocator transientItemLocator)
        {
            _transientItemLocator = transientItemLocator;
        }

        public void HoldLeftHand(string itemName, Action callback)
        {
            if (_transientItemLocator.GetItemByName(itemName).HasSome(out _item))
                _item.HoldLeftHand(callback);
            else
                callback();
        }

        public void HoldRightHand(string itemName, Action callback)
        {
            if (_transientItemLocator.GetItemByName(itemName).HasSome(out _item))
                _item.HoldRightHand(callback);
            else
                callback();
        }

        public void Unhold(Action callback)
        {
            if (_item == null)
                callback();
            else
                _item.ClearFromHands(callback);
        }
    }
}