using System;

namespace Strawhenge.Inventory.Info
{
    public class ItemInfo
    {
        public ItemInfo(
            string itemName,
            string holsterName = null,
            bool isInStorage = false,
            bool isInLeftHand = false,
            bool isInRightHand = false)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentException("Item name is empty.", nameof(itemName));

            ItemName = itemName;
            HolsterName = holsterName ?? string.Empty;
            IsInStorage = isInStorage;
            IsInLeftHand = isInLeftHand;
            IsInRightHand = isInRightHand;
        }

        public string ItemName { get; }

        public string HolsterName { get; }

        public bool IsInStorage { get; }

        public bool IsInLeftHand { get; }

        public bool IsInRightHand { get; }
    }
}