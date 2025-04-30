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
            if (IsItemInHand(itemName, out var item))
                return item;

            if (IsItemInHolster(itemName, out item))
                return item;

            if (IsItemInStorage(itemName, out item))
                return item;

            return Maybe.None<Item>();
        }

        bool IsItemInHand(string itemName, out Item item) =>
            IsItemContained(_hands.RightHand, itemName, out item) ||
            IsItemContained(_hands.LeftHand, itemName, out item);

        bool IsItemInHolster(string itemName, out Item item)
        {
            foreach (var holster in _holsters)
                if (IsItemContained(holster, itemName, out item))
                    return true;

            item = null;
            return false;
        }

        bool IsItemInStorage(string itemName, out Item item)
        {
            foreach (var storedItem in _storedItems.Items)
                if (Matches(storedItem, itemName))
                {
                    item = storedItem;
                    return true;
                }

            item = null;
            return false;
        }

        bool IsItemContained(ItemContainer itemContainer, string itemName, out Item item) =>
            itemContainer.CurrentItem
                .Where(x => Matches(x, itemName))
                .HasSome(out item);

        static bool Matches(Item item, string itemName) =>
            item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase);
    }
}