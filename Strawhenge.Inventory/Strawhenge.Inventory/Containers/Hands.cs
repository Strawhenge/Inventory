using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Containers
{
    public class Hands : IHands
    {
        readonly ItemContainer _leftHand = new ItemContainer("Left Hand");
        readonly ItemContainer _rightHand = new ItemContainer("Right Hand");

        public IItemContainer LeftHand => _leftHand;

        public IItemContainer RightHand => _rightHand;

        public Maybe<Item> ItemInLeftHand => _leftHand.CurrentItem;

        public Maybe<Item> ItemInRightHand => _rightHand.CurrentItem;

        public bool HasTwoHandedItem(out Item item)
        {
            if (_leftHand.CurrentItem.HasSome(out var leftItem) && leftItem.IsTwoHanded)
            {
                item = leftItem;
                return true;
            }

            if (_rightHand.CurrentItem.HasSome(out var rightItem) && rightItem.IsTwoHanded)
            {
                item = rightItem;
                return true;
            }

            item = null;
            return false;
        }

        public bool IsInLeftHand(Item item) => _leftHand.IsCurrentItem(item);

        public bool IsInRightHand(Item item) => _rightHand.IsCurrentItem(item);

        public void SetItemLeftHand(Item item)
        {
            _leftHand.SetItem(item);
        }

        public void SetItemRightHand(Item item)
        {
            _rightHand.SetItem(item);
        }

        public void UnsetItemLeftHand()
        {
            _leftHand.UnsetItem();
        }

        public void UnsetItemRightHand()
        {
            _rightHand.UnsetItem();
        }
    }
}