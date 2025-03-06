using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Containers
{
    public class Hands : IHands
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

        public void SetItemLeftHand(Item item)
        {
            LeftHand.SetItem(item);
        }

        public void SetItemRightHand(Item item)
        {
            RightHand.SetItem(item);
        }

        public void UnsetItemLeftHand()
        {
            LeftHand.UnsetItem();
        }

        public void UnsetItemRightHand()
        {
            RightHand.UnsetItem();
        }
    }
}