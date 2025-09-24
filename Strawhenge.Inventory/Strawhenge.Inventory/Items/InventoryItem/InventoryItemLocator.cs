using FunctionalUtilities;

namespace Strawhenge.Inventory.Items
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

        public Maybe<InventoryItem> Locate(Item item)
        {
            if (IsItemInHand(item, out var inventoryItem))
                return inventoryItem;

            if (IsItemInHolster(item, out inventoryItem))
                return inventoryItem;

            if (IsItemInStorage(item, out inventoryItem))
                return inventoryItem;

            return Maybe.None<InventoryItem>();
        }

        bool IsItemInHand(Item item, out InventoryItem inventoryItem) =>
            IsItemContained(_hands.RightHand, item, out inventoryItem) ||
            IsItemContained(_hands.LeftHand, item, out inventoryItem);

        bool IsItemInHolster(Item item, out InventoryItem inventoryItem)
        {
            foreach (var holster in _holsters)
                if (IsItemContained(holster, item, out inventoryItem))
                    return true;

            inventoryItem = null;
            return false;
        }

        bool IsItemInStorage(Item item, out InventoryItem inventoryItem)
        {
            foreach (var storedItem in _storedItems.Items)
                if (Matches(storedItem, item))
                {
                    inventoryItem = storedItem;
                    return true;
                }

            inventoryItem = null;
            return false;
        }

        static bool IsItemContained(ItemContainer itemContainer, Item item, out InventoryItem inventoryItem) =>
            itemContainer.CurrentItem
                .Where(x => Matches(x, item))
                .HasSome(out inventoryItem);

        static bool Matches(InventoryItem inventoryItem, Item item) => inventoryItem.Item == item;
    }
}