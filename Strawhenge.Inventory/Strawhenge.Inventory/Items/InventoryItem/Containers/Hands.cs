using FunctionalUtilities;

namespace Strawhenge.Inventory.Items
{
    public class Hands
    {
        public ItemContainer LeftHand { get; } = new ItemContainer("Left Hand");

        public ItemContainer RightHand { get; } = new ItemContainer("Right Hand");

        public Maybe<InventoryItem> ItemInLeftHand => LeftHand.CurrentItem;

        public Maybe<InventoryItem> ItemInRightHand => RightHand.CurrentItem;

        public bool HasTwoHandedItem(out InventoryItem item)
        {
            if (LeftHand.CurrentItem.HasSome(out var leftItem) && leftItem.IsTwoHanded)
            {
                item = leftItem;
                return true;
            }

            if (RightHand.CurrentItem.HasSome(out var rightItem) && rightItem.IsTwoHanded)
            {
                item = rightItem;
                return true;
            }

            item = null;
            return false;
        }

        public bool IsInLeftHand(InventoryItem item) => LeftHand.IsCurrentItem(item);

        public bool IsInRightHand(InventoryItem item) => RightHand.IsCurrentItem(item);

        internal void SetItemLeftHand(InventoryItem item)
        {
            LeftHand.SetItem(item);
        }

        internal void SetItemRightHand(InventoryItem item)
        {
            RightHand.SetItem(item);
        }

        internal void UnsetItemLeftHand()
        {
            LeftHand.UnsetItem();
        }

        internal void UnsetItemRightHand()
        {
            RightHand.UnsetItem();
        }
    }
}