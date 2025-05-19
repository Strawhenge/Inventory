using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using System;

namespace Strawhenge.Inventory.Loader
{
    public class LoadInventoryItem
    {
        public LoadInventoryItem(Item itemData)
        {
            ItemData = itemData ?? throw new ArgumentNullException(nameof(itemData));
        }

        public Item ItemData { get; }

        public Maybe<string> HolsterName { get; private set; } = Maybe.None<string>();

        public bool IsInStorage { get; set; }

        public LoadInventoryItemInHand InHand { get; set; }

        public void SetHolster(string holsterName)
        {
            if (string.IsNullOrWhiteSpace(holsterName))
                throw new ArgumentException("Holster name not valid.", paramName: nameof(holsterName));

            HolsterName = holsterName;
        }
    }
}