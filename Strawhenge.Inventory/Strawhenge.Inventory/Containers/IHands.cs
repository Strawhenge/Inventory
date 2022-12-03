using FunctionalUtilities;

namespace Strawhenge.Inventory.Containers
{
    public interface IHands
    {
        Maybe<IItem> ItemInLeftHand { get; }

        Maybe<IItem> ItemInRightHand { get; }

        void SetItemLeftHand(IItem item);

        void SetItemRightHand(IItem item);

        void UnsetItemLeftHand();

        void UnsetItemRightHand();

        bool HasTwoHandedItem(out IItem item);

        bool IsInLeftHand(IItem item);

        bool IsInRightHand(IItem item);
    }
}
