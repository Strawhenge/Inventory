using FunctionalUtilities;

namespace Strawhenge.Inventory.Containers
{
    public interface IItemContainer
    {
        string Name { get; }

        Maybe<IItem> CurrentItem { get; }

        bool IsCurrentItem(IItem item);
    }
}