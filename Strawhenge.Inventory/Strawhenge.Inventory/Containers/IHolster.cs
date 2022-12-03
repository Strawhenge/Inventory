using FunctionalUtilities;

namespace Strawhenge.Inventory.Containers
{
    public interface IHolster
    {
        string Name { get; }

        Maybe<IItem> CurrentItem { get; }

        bool IsCurrentItem(IItem item);

        void SetItem(IItem item);

        void UnsetItem();
    }
}