using FunctionalUtilities;
using Strawhenge.Inventory.Unity.Data;
using System;

namespace Strawhenge.Inventory.Unity.NewLoader
{
    public class LoadInventoryItemDto : ILoadInventoryItem
    {
        public LoadInventoryItemDto(IItemData itemData)
        {
            ItemData = itemData ?? throw new ArgumentNullException(nameof(itemData));
        }

        public IItemData ItemData { get; }

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