using Strawhenge.Inventory.Unity.Items;

namespace Strawhenge.Inventory.Unity.Components
{
    public interface IHolsterComponent
    {
        string Name { get; }

        void SetItem(IItemHelper item);

        IItemHelper TakeItem();
    }
}