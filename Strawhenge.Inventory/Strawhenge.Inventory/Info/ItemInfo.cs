using System;

namespace Strawhenge.Inventory.Info
{
    public class ItemInfo
    {
        public ItemInfo(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentException("Item name is empty.", nameof(itemName));

            ItemName = itemName;
        }

        public string ItemName { get; }

        public string HolsterName { get; internal set; } = string.Empty;

        public bool IsInStorage { get; internal set; }

        public bool IsInLeftHand { get; internal set; }

        public bool IsInRightHand { get; internal set; }
    }
}