namespace Strawhenge.Inventory.Containers
{
    public class Hands : IHands
    {
        public Maybe<IItem> ItemInLeftHand { get; private set; } = Maybe.None<IItem>();

        public Maybe<IItem> ItemInRightHand { get; private set; } = Maybe.None<IItem>();

        public bool HasTwoHandedItem(out IItem item)
        {
            if (ItemInLeftHand.HasSome(out var leftItem) && leftItem.IsTwoHanded)
            {
                item = leftItem;
                return true;
            }

            if (ItemInRightHand.HasSome(out var rightItem) && rightItem.IsTwoHanded)
            {
                item = rightItem;
                return true;
            }

            item = null;
            return false;
        }

        public bool IsInLeftHand(IItem item)
        {
            return ItemInLeftHand.HasSome(out var leftItem) &&
                leftItem == item;
        }

        public bool IsInRightHand(IItem item)
        {
            return ItemInRightHand.HasSome(out var rightItem) &&
                rightItem == item;
        }

        public void SetItemLeftHand(IItem item)
        {
            ItemInLeftHand = Maybe.Some(item);
        }

        public void SetItemRightHand(IItem item)
        {
            ItemInRightHand = Maybe.Some(item);
        }

        public void UnsetItemLeftHand()
        {
            ItemInLeftHand = Maybe.None<IItem>();
        }

        public void UnsetItemRightHand()
        {
            ItemInRightHand = Maybe.None<IItem>();
        }
    }
}
