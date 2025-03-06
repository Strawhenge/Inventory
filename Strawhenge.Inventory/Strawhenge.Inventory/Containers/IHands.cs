using FunctionalUtilities;
using Strawhenge.Inventory.Items;

namespace Strawhenge.Inventory.Containers
{
    public interface IHands
    {
        ItemContainer LeftHand { get; }

        ItemContainer RightHand { get; }

        Maybe<Item> ItemInLeftHand { get; }

        Maybe<Item> ItemInRightHand { get; }

        void SetItemLeftHand(Item item);

        void SetItemRightHand(Item item);

        void UnsetItemLeftHand();

        void UnsetItemRightHand();

        bool HasTwoHandedItem(out Item item);

        bool IsInLeftHand(Item item);

        bool IsInRightHand(Item item);
    }
}