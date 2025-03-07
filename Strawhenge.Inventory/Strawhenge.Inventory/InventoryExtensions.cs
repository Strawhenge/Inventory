using System.Collections.Generic;
using System.Linq;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory
{
    public static class InventoryExtensions
    {
        public static IEnumerable<Item> AllItems(this IInventory inventory)
        {
            return inventory.LeftHand.CurrentItem.AsEnumerable()
                .Concat(inventory.RightHand.CurrentItem.AsEnumerable())
                .Concat(inventory.Holsters.SelectMany(x => x.CurrentItem.AsEnumerable()))
                .Concat(inventory.StoredItems.Items)
                .Distinct();
        }

        public static void SwapHands(this IInventory inventory, bool ignoreTwoHanded = false)
        {
            if (ignoreTwoHanded && inventory.IsHoldingTwoHandedItem())
                return;

            if (inventory.RightHand.CurrentItem.HasSome(out var item) ||
                inventory.LeftHand.CurrentItem.HasSome(out item))
            {
                item.SwapHands();
            }
        }

        public static bool IsHoldingTwoHandedItem(this IInventory inventory)
        {
            return (inventory.RightHand.CurrentItem.HasSome(out var item) && item.IsTwoHanded) ||
                   (inventory.LeftHand.CurrentItem.HasSome(out item) && item.IsTwoHanded);
        }
    }
}