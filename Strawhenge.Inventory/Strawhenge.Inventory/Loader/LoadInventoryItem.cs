using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using System;

namespace Strawhenge.Inventory.Loader
{
    public class LoadInventoryItem
    {
        public LoadInventoryItem(Item item)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public Item Item { get; }

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