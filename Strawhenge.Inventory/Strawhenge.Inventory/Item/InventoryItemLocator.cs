using System;
using FunctionalUtilities;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory
{
    class InventoryItemLocator
    {
        readonly Hands _hands;
        readonly Holsters _holsters;
        readonly StoredItems _storedItems;

        public InventoryItemLocator(Hands hands, Holsters holsters, StoredItems storedItems)
        {
            _hands = hands;
            _holsters = holsters;
            _storedItems = storedItems;
        }

        public Maybe<InventoryItem> Locate(string itemName)
        {
            if (IsItemInHand(itemName, out var item))
                return item;

            if (IsItemInHolster(itemName, out item))
                return item;

            if (IsItemInStorage(itemName, out item))
                return item;

            return Maybe.None<InventoryItem>();
        }

        bool IsItemInHand(string itemName, out InventoryItem item) =>
            IsItemContained(_hands.RightHand, itemName, out item) ||
            IsItemContained(_hands.LeftHand, itemName, out item);

        bool IsItemInHolster(string itemName, out InventoryItem item)
        {
            foreach (var holster in _holsters)
                if (IsItemContained(holster, itemName, out item))
                    return true;

            item = null;
            return false;
        }

        bool IsItemInStorage(string itemName, out InventoryItem item)
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

        bool IsItemContained(ItemContainer itemContainer, string itemName, out InventoryItem item) =>
            itemContainer.CurrentItem
                .Where(x => Matches(x, itemName))
                .HasSome(out item);

        static bool Matches(InventoryItem item, string itemName) =>
            item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase);
    }
}