using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Containers
{
    public class Hands
    {
        public ItemContainer LeftHand { get; } = new ItemContainer("Left Hand");

        public ItemContainer RightHand { get; } = new ItemContainer("Right Hand");

        public Maybe<Item> ItemInLeftHand => LeftHand.CurrentItem;

        public Maybe<Item> ItemInRightHand => RightHand.CurrentItem;

        public bool HasTwoHandedItem(out Item item)
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

        public bool IsInLeftHand(Item item) => LeftHand.IsCurrentItem(item);

        public bool IsInRightHand(Item item) => RightHand.IsCurrentItem(item);

        internal void SetItemLeftHand(Item item)
        {
            LeftHand.SetItem(item);
        }

        internal void SetItemRightHand(Item item)
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