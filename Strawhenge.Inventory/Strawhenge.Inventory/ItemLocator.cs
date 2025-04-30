using System;
using FunctionalUtilities;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory
{
    class ItemLocator
    {
        readonly Hands _hands;
        readonly Holsters _holsters;
        readonly StoredItems _storedItems;

        public ItemLocator(Hands hands, Holsters holsters, StoredItems storedItems)
        {
            _hands = hands;
            _holsters = holsters;
            _storedItems = storedItems;
        }

        public Maybe<Item> Locate(string itemName)
        {
            if (_hands.ItemInRightHand
                    .Where(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                    .HasSome(out var item) ||
                _hands.ItemInLeftHand
                    .Where(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                    .HasSome(out item))
                return item;

            foreach (var holster in _holsters)
            {
                if (holster.CurrentItem
                    .Where(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                    .HasSome(out item))
                    return item;
            }

            foreach (var storedItem in _storedItems.Items)
            {
                if (storedItem.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                    return storedItem;
            }

            return Maybe.None<Item>();
        }
    }
}