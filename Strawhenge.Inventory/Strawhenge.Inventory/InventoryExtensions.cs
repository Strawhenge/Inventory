using System.Collections.Generic;
using System.Linq;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory
{
    public static class InventoryExtensions
    {
        public static IEnumerable<InventoryItem> AllItems(this Inventory inventory)
        {
            return inventory.Hands.LeftHand.CurrentItem.AsEnumerable()
                .Concat(inventory.Hands.RightHand.CurrentItem.AsEnumerable())
                .Concat(inventory.Holsters.SelectMany(x => x.CurrentItem.AsEnumerable()))
                .Concat(inventory.StoredItems.Items)
                .Distinct();
        }

        public static void SwapHands(this Inventory inventory, bool ignoreTwoHanded = false)
        {
            if (ignoreTwoHanded && inventory.IsHoldingTwoHandedItem())
                return;

            if (inventory.Hands.RightHand.CurrentItem.HasSome(out var item) ||
                inventory.Hands.LeftHand.CurrentItem.HasSome(out item))
            {
                item.SwapHands();
            }
        }

        public static bool IsHoldingTwoHandedItem(this Inventory inventory)
        {
            return (inventory.Hands.RightHand.CurrentItem.HasSome(out var item) && item.IsTwoHanded) ||
                   (inventory.Hands.LeftHand.CurrentItem.HasSome(out item) && item.IsTwoHanded);
        }
    }
}