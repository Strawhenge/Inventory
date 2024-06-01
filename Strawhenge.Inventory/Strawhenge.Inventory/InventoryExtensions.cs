using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory
{
    public static class InventoryExtensions
    {
        public static IEnumerable<IItem> AllItems(this IInventory inventory)
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

            var leftHandItem = inventory.LeftHand.CurrentItem;
            inventory.RightHand.CurrentItem.Do(item => item.HoldLeftHand());
            leftHandItem.Do(item => item.HoldRightHand());
        }

        public static bool IsHoldingTwoHandedItem(this IInventory inventory)
        {
            return (inventory.RightHand.CurrentItem.HasSome(out var item) && item.IsTwoHanded) ||
                   (inventory.LeftHand.CurrentItem.HasSome(out item) && item.IsTwoHanded);
        }
    }
}