namespace Strawhenge.Inventory
{
    public static class InventoryExtensions
    {
        public static void SwapHands(this IInventory inventory)
        {
            var leftHandItem = inventory.LeftHand.CurrentItem;
            inventory.RightHand.CurrentItem.Do(item => item.HoldLeftHand());
            leftHandItem.Do(item => item.HoldRightHand());
        }
    }
}